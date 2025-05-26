window.home = {
  cartCount: 0,

  increaseQuantity(btn) {
    const d = btn.parentElement.querySelector(".quantity-display");
    let q = parseInt(d.textContent, 10);
    d.textContent = ++q;
  },

  decreaseQuantity(btn) {
    const d = btn.parentElement.querySelector(".quantity-display");
    let q = parseInt(d.textContent, 10);
    if (q > 1) d.textContent = --q;
  },

  addToCart(btn) {
    const d = btn.parentElement.querySelector(".quantity-display");
    const q = parseInt(d.textContent, 10);
    this.cartCount += q;
    document.querySelector(
      ".cart"
    ).innerHTML = `<div class="cart-icon"></div>Cart (${this.cartCount})`;
    d.textContent = "1";
    btn.textContent = "Added!";
    btn.style.background = "#34a853";
    setTimeout(() => {
      btn.textContent = "Add to Cart";
      btn.style.background = "#34a853";
    }, 1000);
  },

  changePerPage(v) {
    document.getElementById("perPageBtn").textContent = `${v} per page`;
    console.log(`Per page changed to ${v}`);
  },

  openModal() {
    const m = document.getElementById("modalOverlay");
    m.classList.add("active");
    document.body.style.overflow = "hidden";
  },

  closeModal() {
    const m = document.getElementById("modalOverlay");
    m.classList.remove("active");
    document.body.style.overflow = "auto";
    document.getElementById("addProductForm").reset();
  },

  closeModalOnOverlay(e) {
    if (e.target === e.currentTarget) this.closeModal();
  },

  generateSlug() {
    const n = document.getElementById("productName").value;
    const slug = n
      .toLowerCase()
      .replace(/[^a-z0-9\s-]/g, "")
      .trim()
      .replace(/\s+/g, "-");
    document.getElementById("productSlug").value = slug;
  },

  submitProduct() {
    const f = document.getElementById("addProductForm");
    const data = {
      name: f.productName.value,
      slug: f.productSlug.value,
      price: f.productPrice.value,
      discountStart: f.discountStart.value,
      discountEnd: f.discountEnd.value,
    };
    if (!data.name || !data.slug || !data.price) {
      alert("Fill required");
      return;
    }
    console.log("Submitted", data);
    const b = document.querySelector(".add-btn");
    b.textContent = "Added!";
    b.style.background = "#34a853";
    setTimeout(() => {
      this.closeModal();
      b.textContent = "Add";
      b.style.background = "#4285f4";
    }, 1500);
  },

  // Cart sidebar
  openCartSidebar() {
    document.getElementById("cartSidebarOverlay").classList.add("active");
    document.body.style.overflow = "hidden";
  },
  closeCartSidebar(e) {
    if (e && e.target !== e.currentTarget) return;
    document.getElementById("cartSidebarOverlay").classList.remove("active");
    document.body.style.overflow = "auto";
  },
  updateCartQuantity(btn, change) {
    const num = btn.parentElement.querySelector(".qty-number");
    let q = parseInt(num.textContent, 10) + change;
    num.textContent = q < 1 ? 1 : q;
    this.updateCartSubtotal();
  },
  updateCartSubtotal() {
    let t = 0;
    document.querySelectorAll(".cart-item").forEach((i) => {
      const p = parseFloat(
        i.querySelector(".cart-item-price").textContent.replace("$", "")
      );
      const q = parseInt(i.querySelector(".qty-number").textContent, 10);
      t += p * q;
    });
    document.getElementById("cartSubtotal").textContent = `$${t.toFixed(2)}`;
  },

  prevPage() {
    /*TODO*/
  },
  nextPage() {
    /*TODO*/
  },
  goToPage(n) {
    /*TODO*/
  },
};

// Attach cart click
document.addEventListener("DOMContentLoaded", () => {
  document
    .querySelector(".cart")
    .addEventListener("click", home.openCartSidebar);
});
// Escape key handlers
document.addEventListener("keydown", (e) => {
  if (e.key === "Escape") {
    home.closeModal();
    home.closeCartSidebar();
  }
});
