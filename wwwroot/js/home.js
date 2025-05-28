// wwwroot/js/home.js

window.home = {
  state: {
    pageSize: null,
    currentPage: null,
    totalResults: null,
    searchText: null,
    cartCount: 0,
    cartItems: [],
  },

  init() {
    // Read initial values from URL if present
    const params = new URLSearchParams(window.location.search);
    this.state.currentPage =
      parseInt(params.get("pageNumber")) || window.initialData.currentPage;
    this.state.pageSize =
      parseInt(params.get("pageSize")) || window.initialData.pageSize;
    this.state.searchText =
      params.get("search") || window.initialData.searchText;
    this.state.totalResults = window.initialData.totalResults;

    // Initialize UI
    const searchEl = document.getElementById("searchInput");
    searchEl.value = this.state.searchText;
    document.getElementById(
      "perPageBtn"
    ).textContent = `${this.state.pageSize} per page`;

    // Debounced search: 300ms delay & min 2 characters
    let debounceTimer;
    searchEl.addEventListener("keyup", () => {
      clearTimeout(debounceTimer);
      const value = searchEl.value.trim();
      debounceTimer = setTimeout(() => {
        if (value.length >= 2 || value.length === 0) {
          this.state.searchText = value;
          this.state.currentPage = 1;
          this.updateUrl();
          this.fetchData();
        }
      }, 300);
    });

    const nameInput = document.getElementById("productName");
    if (nameInput) {
      nameInput.addEventListener("input", () => this.generateSlug());
    }

    this.fetchData(); // initial render
    this.bindPerDropdown();
    this.updateCartCount(); // <-- fetch and show cart count on load
    document.querySelector(".cart").addEventListener("click", () => {
      this.openCartSidebar();
      this.fetchCartItems();
    });
  },

  generateSlug() {
    const nameEl = document.getElementById("productName");
    const slugEl = document.getElementById("productSlug");
    if (!nameEl || !slugEl) return;
    const slug = nameEl.value
      .toLowerCase()
      .replace(/[^a-z0-9\s-]/g, "")
      .trim()
      .replace(/\s+/g, "-");
    slugEl.value = slug;
  },

  changePerPage(v) {
    this.state.pageSize = v;
    document.getElementById("perPageBtn").textContent = `${v} per page`;
    this.hidePerDropdown();
    this.state.currentPage = 1;
    this.updateUrl();
    this.fetchData();
  },

  prevPage() {
    if (this.state.currentPage > 1) {
      this.state.currentPage--;
      this.updateUrl();
      this.fetchData();
    }
  },

  nextPage() {
    const maxPage = Math.ceil(this.state.totalResults / this.state.pageSize);
    if (this.state.currentPage < maxPage) {
      this.state.currentPage++;
      this.updateUrl();
      this.fetchData();
    }
  },

  goToPage(n) {
    this.state.currentPage = n;
    this.updateUrl();
    this.fetchData();
  },

  updateUrl() {
    const params = new URLSearchParams();
    params.set("pageNumber", this.state.currentPage);
    params.set("pageSize", this.state.pageSize);
    if (this.state.searchText) params.set("search", this.state.searchText);
    const newUrl = `${window.location.pathname}?${params.toString()}`;
    history.replaceState(null, "", newUrl);
  },

  fetchData() {
    const { currentPage, pageSize, searchText } = this.state;
    fetch(
      `?pageNumber=${currentPage}&pageSize=${pageSize}&search=${encodeURIComponent(
        searchText
      )}`,
      {
        headers: { "X-Requested-With": "XMLHttpRequest" },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        this.state.totalResults = data.totalResults;
        this.renderProducts(data.items);
        this.renderPagination();
      });
  },

  renderProducts(items) {
    const grid = document.getElementById("productsGrid");
    grid.innerHTML = items
      .map((p) => {
        const price = p.price;
        const discounted = p.discountedPrice;
        const priceHtml =
          discounted < price
            ? `<span class="discounted-price">${discounted.toLocaleString(
                "en-US",
                { style: "currency", currency: "USD" }
              )}</span>
           <span class="original-price" style="text-decoration: line-through; color: #999; margin-left:8px;">
             ${price.toLocaleString("en-US", {
               style: "currency",
               currency: "USD",
             })}
           </span>`
            : `<span class="price">${price.toLocaleString("en-US", {
                style: "currency",
                currency: "USD",
              })}</span>`;

        return `
        <div class="product-card" data-product-id="${p.productId}">
          <img src="https://images.unsplash.com/photo-1473968512647-3e447244af8f?w=400&h=300&fit=crop&crop=center" alt="DJI Phantom 3 Vision" class="product-image">          
          <div class="product-content">
            <div class="product-name">${p.productName}</div>
            <div class="product-price">${priceHtml}</div>
            <div class="cart-controls">
              <div class="quantity-controls">
                <div class="quantity-row">
                  <button class="quantity-btn" onclick="home.decreaseQuantity(this)">-</button>
                  <span class="quantity-display">1</span>
                  <button class="quantity-btn" onclick="home.increaseQuantity(this)">+</button>
                </div>
                <button class="confirm-add-btn" onclick="home.addToCart(this)">Add to Cart</button>
              </div>
              <button class="add-to-cart">Add to cart</button>
            </div>
          </div>
        </div>`;
      })
      .join("");

    document.getElementById("resultsInfo").textContent = `Showing ${
      (this.state.currentPage - 1) * this.state.pageSize + 1
    }–${Math.min(
      this.state.currentPage * this.state.pageSize,
      this.state.totalResults
    )} of ${this.state.totalResults} results`;
  },

  renderPagination() {
    const container = document.getElementById("pageButtons");
    const totalPages = Math.ceil(this.state.totalResults / this.state.pageSize);
    let html = "";

    for (let i = 1; i <= totalPages; i++) {
      if (i === 6 && totalPages > 7) {
        html += '<span class="dots">…</span>';
        i = totalPages - 2;
      }
      html += `<button class="${
        i === this.state.currentPage ? "active" : ""
      }" onclick="home.goToPage(${i})">${i}</button>`;
    }
    container.innerHTML = html;
  },

  bindPerDropdown() {
    document.addEventListener("click", (e) => {
      if (!e.target.closest(".per-page-select")) this.hidePerDropdown();
    });
  },

  togglePerDropdown() {
    document.getElementById("perDropdown").classList.toggle("active");
  },

  hidePerDropdown() {
    document.getElementById("perDropdown").classList.remove("active");
  },

  increaseQuantity(btn) {
    const d = btn.parentElement.querySelector(".quantity-display");
    d.textContent = parseInt(d.textContent, 10) + 1;
  },

  decreaseQuantity(btn) {
    const d = btn.parentElement.querySelector(".quantity-display");
    let q = parseInt(d.textContent, 10);
    if (q > 1) d.textContent = q - 1;
  },

  addToCart(btn) {
    const card = btn.closest(".product-card");
    const productId = card.dataset.productId;
    const quantity = parseInt(
      card.querySelector(".quantity-display").textContent,
      10
    );

    console.log(productId, quantity);
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
          this.state.cartCount += 1;
          document.querySelector(
            ".cart"
          ).innerHTML = `<div class="cart-icon"></div>Cart (${this.state.cartCount})`;
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
  },

  updateCartCount() {
    fetch("/api/Carts")
      .then((r) => {
        if (!r.ok) throw new Error("Failed to fetch cart");
        return r.json();
      })
      .then((cartData) => {
        // Assuming cartData is an array of cart items
        const count = cartData.data.items ? cartData.data.items.length : 0;
        console.log(count);
        this.state.cartCount = count;
        const el = document.getElementById("cartCount");
        if (el) el.textContent = count;
      })
      .catch(console.error);
  },

  fetchCartItems() {
    fetch("/api/Carts")
      .then((r) => (r.ok ? r.json() : Promise.reject()))
      .then((cartData) => {
        // extract items array
        this.state.cartItems = Array.isArray(cartData)
          ? cartData
          : cartData.data && Array.isArray(cartData.data.items)
          ? cartData.data.items
          : [];
        this.renderCartItems();
      })
      .catch((err) => {
        console.error("Failed to load cart items", err);
        document.getElementById("cartItems").innerHTML =
          "<p class='empty'>Unable to load cart.</p>";
      });
  },

  renderCartItems() {
    const container = document.getElementById("cartItems");
    if (!this.state.cartItems.length) {
      container.innerHTML = "<p class='empty'>Your cart is empty.</p>";
      return;
    }

    container.innerHTML = this.state.cartItems
      .map((item) => {
        const p = item.product;
        // Choose the lower of price vs discountedPrice:
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
          <img src="https://images.unsplash.com/photo-1473968512647-3e447244af8f?w=400&h=300&fit=crop&crop=center" alt="DJI Phantom 3 Vision" class="cart-item-image">          
          <div class="cart-item-details">
            <div class="cart-item-name">${p.productName}</div>
            <div class="cart-item-price">${priceHtml}</div>
            <div class="cart-item-quantity">
              <button class="qty-btn" onclick="home.updateCartQuantity(this,-1)">-</button>
              <span class="qty-number">${item.quantity}</span>
              <button class="qty-btn" onclick="home.updateCartQuantity(this,1)">+</button>
            </div>
          </div>
        </div>`;
      })
      .join("");

    // update subtotal after rendering:
    this.updateCartSubtotal();
  },
  updateCartQuantity(btn, delta) {
    const root = btn.closest(".cart-item");
    const cartId = root.dataset.cartId;
    let qty =
      parseInt(root.querySelector(".qty-number").textContent, 10) + delta;
    if (qty < 1) qty = 1;

    // call backend to update quantity
    fetch(`/api/Carts/${cartId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ quantity: qty }),
    })
      .then((r) => (r.ok ? r.json() : Promise.reject()))
      .then(() => {
        root.querySelector(".qty-number").textContent = qty;
        this.updateCartCount();
        this.updateCartSubtotal();
      })
      .catch((err) => console.error("Quantity update failed", err));
  },
  updateCartSubtotal() {
    let total = 0;
    this.state.cartItems.forEach((item) => {
      const p = item.product;
      const unitPrice =
        p.discountedPrice < p.price ? p.discountedPrice : p.price;
      total += unitPrice * item.quantity;
    });
    document.getElementById("cartSubtotal").textContent = total.toLocaleString(
      "en-US",
      { style: "currency", currency: "USD" }
    );
  },

  submitProduct() {
    const f = document.getElementById("addProductForm");
    const errContainer = document.getElementById("formErrors");
    errContainer.innerHTML = ""; 
    // Helper to build a Date with +1 minute offset
    const makeDateTime = (dateString) => {
      const today = new Date();
      const selected = new Date(dateString); // at midnight local
      const isToday = selected.toDateString() === today.toDateString();

      if (isToday) {
        // now + 1 minute
        return new Date(Date.now() + 60_000);
      } else {
        // date at midnight local, then +1 minute
        selected.setHours(0, 1, 0, 0);
        return selected;
      }
    };

    const startDt = makeDateTime(f.discountStart.value);
    const endDt = makeDateTime(f.discountEnd.value);

    const data = {
      productName: f.productName.value.trim(),
      slug: f.productSlug.value.trim(),
      productImage: f.productImage.value.trim(),
      price: parseFloat(f.productPrice.value),
      discountStart: startDt.toISOString(),
      discountEnd: endDt.toISOString(),
    };

    // Validation
    if (
      !data.productName ||
      !data.slug ||
      !data.productImage ||
      isNaN(data.price)
    ) {
      return this.showToast("Please fill in all fields correctly", "error");
    }

    fetch("/api/Products", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    })
      .then(async (res) => {
        const body = await res.json();
        if (res.ok) {
          this.showToast("Product added successfully!", "success");
          this.closeModal();
          this.state.currentPage = 1;
          this.fetchData();
        } else {
          console.log(body)
          // Validation errors
          if (body.errors && Array.isArray(body.errors)) {
            // Build a list of messages
            const msgs = body.errors.flatMap((e) =>
              e.errors.map(
                (msg) => `<li><strong>${e.field}:</strong> ${msg}</li>`
              )
            );
            errContainer.innerHTML = `<ul style="margin:0; padding-left:1.25rem;">${msgs.join(
              ""
            )}</ul>`;
          } else {
            // Other server error
            errContainer.textContent = body.message || "Failed to add product.";
          }
        }
      })
      .catch((err) => {
        console.error(err);
        this.showToast("Network error while adding product", "error");
      });
  },

  showToast(message, type) {
    const container = document.getElementById("toast-container");
    const toast = document.createElement("div");
    toast.textContent = message;
    toast.style.cssText = `
      padding: .75rem 1rem;
      border-radius: .375rem;
      background: ${type === "success" ? "#4caf50" : "#f44336"};
      color: white;
      box-shadow: 0 2px 6px rgba(0,0,0,0.2);
      font-weight: 500;
    `;
    container.appendChild(toast);
    setTimeout(() => {
      toast.style.transition = "opacity .3s";
      toast.style.opacity = "0";
      setTimeout(() => container.removeChild(toast), 300);
    }, 2000);
  },

  openModal() {
    document.getElementById("modalOverlay").classList.add("active");
    document.body.style.overflow = "hidden";
  },

  closeModal() {
    document.getElementById("modalOverlay").classList.remove("active");
    document.body.style.overflow = "auto";
    document.getElementById("addProductForm").reset();
  },

  closeModalOnOverlay(e) {
    if (e.target === e.currentTarget) this.closeModal();
  },

  openCartSidebar() {
    document.getElementById("cartSidebarOverlay").classList.add("active");
    document.body.style.overflow = "hidden";
  },

  closeCartSidebar(e) {
    if (e && e.target !== e.currentTarget) return;
    document.getElementById("cartSidebarOverlay").classList.remove("active");
    document.body.style.overflow = "auto";
  },
};

document.addEventListener("DOMContentLoaded", () => home.init());
