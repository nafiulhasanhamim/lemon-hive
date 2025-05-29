import { state } from "./state.js";
import { showToast } from "./utilities.js";

export function updateCartCount() {
  fetch("/api/Carts")
    .then((r) => {
      if (!r.ok) throw new Error("Failed to fetch cart");
      return r.json();
    })
    .then((cartData) => {
      const count = cartData.data?.items?.length || 0;
      state.cartCount = count;
      const el = document.getElementById("cartCount");
      if (el) el.textContent = count;
    })
    .catch(console.error);
}

export function fetchCartItems() {
  fetch("/api/Carts")
    .then((r) => (r.ok ? r.json() : Promise.reject()))
    .then((cartData) => {
      state.cartItems = cartData.data?.items || [];
      renderCartItems();
    })
    .catch((err) => {
      console.error("Failed to load cart items", err);
      const container = document.getElementById("cartItems");
      if (container) {
        container.innerHTML = "<p class='empty'>Unable to load cart.</p>";
      }
    });
}

export function renderCartItems() {
  const container = document.getElementById("cartItems");
  if (!container) return;

  if (!state.cartItems.length) {
    container.innerHTML = "<p class='empty'>Your cart is empty.</p>";
    return;
  }

  container.innerHTML = state.cartItems
    .map((item) => {
      const p = item.product;
      const unitPrice =
        p.discountedPrice < p.price ? p.discountedPrice : p.price;
      const priceHtml =
        p.discountedPrice < p.price
          ? `<span class="discounted-price">${unitPrice.toLocaleString(
              "en-US",
              { style: "currency", currency: "USD" }
            )}</span>
                  <span class="original-price" style="text-decoration: line-through; color: #999; margin-left:8px;">
                      ${p.price.toLocaleString("en-US", {
                        style: "currency",
                        currency: "USD",
                      })}
                  </span>`
          : `<span class="price">${unitPrice.toLocaleString("en-US", {
              style: "currency",
              currency: "USD",
            })}</span>`;

      return `
            <div class="cart-item" data-cart-id="${item.cartId}">
                <img src="https://images.unsplash.com/photo-1473968512647-3e447244af8f?w=400&h=300&fit=crop&crop=center" alt="Product" class="cart-item-image">          
                <div class="cart-item-details">
                    <div class="cart-item-name">${p.productName}</div>
                    <div class="cart-item-price">${priceHtml}</div>
                    <div class="cart-item-quantity">
                        <button class="qty-btn" data-action="cart-decrease">-</button>
                        <span class="qty-number">${item.quantity}</span>
                        <button class="qty-btn" data-action="cart-increase">+</button>
                    </div>
                </div>
            </div>`;
    })
    .join("");

  updateCartSubtotal();
}

export function updateCartQuantity(btn, delta) {
  const root = btn.closest(".cart-item");
  const cartId = root.dataset.cartId;
  let qty = parseInt(root.querySelector(".qty-number").textContent) + delta;
  if (qty < 1) qty = 1;

  fetch(`/api/Carts/${cartId}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ quantity: qty }),
  })
    .then((r) => (r.ok ? r.json() : Promise.reject()))
    .then(() => {
      root.querySelector(".qty-number").textContent = qty;
      updateCartCount();
      updateCartSubtotal();
    })
    .catch((err) => console.error("Quantity update failed", err));
}

export function updateCartSubtotal() {
  let total = 0;
  state.cartItems.forEach((item) => {
    const p = item.product;
    const unitPrice = p.discountedPrice < p.price ? p.discountedPrice : p.price;
    total += unitPrice * item.quantity;
  });
  const subtotalEl = document.getElementById("cartSubtotal");
  if (subtotalEl) {
    subtotalEl.textContent = total.toLocaleString("en-US", {
      style: "currency",
      currency: "USD",
    });
  }
}

export function addToCart(btn) {
  const card = btn.closest(".product-card");
  const productId = card.dataset.productId;
  const quantity = parseInt(
    card.querySelector(".quantity-display").textContent
  );

  fetch("/api/Carts", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ productId, quantity }),
  })
    .then((r) => r.json())
    .then((res) => {
      const msgBox = document.createElement("div");
      msgBox.className = "cart-message";
      if (res.success) {
        state.cartCount += 1;
        const cartEl = document.querySelector(".cart");
        if (cartEl) {
          cartEl.innerHTML = `<div class="cart-icon"></div>Cart (${state.cartCount})`;
        }
        msgBox.textContent = "✅ Added to cart!";
        msgBox.style.background = "#4CAF50";
      } else {
        msgBox.textContent = `⚠️ ${res.message}`;
        msgBox.style.background = "#f44336";
      }
      document.body.appendChild(msgBox);
      setTimeout(() => msgBox.remove(), 3000);
    })
    .catch(() => {
      const msgBox = document.createElement("div");
      msgBox.className = "cart-message";
      msgBox.textContent = "❌ Error adding to cart.";
      msgBox.style.background = "#f44336";
      document.body.appendChild(msgBox);
      setTimeout(() => msgBox.remove(), 3000);
    });
}

export function openCartSidebar() {
  const overlay = document.getElementById("cartSidebarOverlay");
  if (overlay) {
    overlay.classList.add("active");
    document.body.style.overflow = "hidden";
    fetchCartItems();
  }
}

export function closeCartSidebar(e) {
  if (e && e.target !== e.currentTarget) return;
  const overlay = document.getElementById("cartSidebarOverlay");
  if (overlay) {
    overlay.classList.remove("active");
    document.body.style.overflow = "auto";
  }
}
