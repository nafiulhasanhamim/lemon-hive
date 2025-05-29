import { state } from "./state.js";
import {
  updateUrl,
  generateSlug,
  showToast,
  bindPerDropdown,
  hidePerDropdown,
  togglePerDropdown,
} from "./utilities.js";
import {
  fetchData,
  renderProducts,
  renderPagination,
  changePerPage,
  prevPage,
  nextPage,
  goToPage,
  applyFilters,
} from "./products.js";
import {
  updateCartCount,
  fetchCartItems,
  renderCartItems,
  updateCartQuantity,
  updateCartSubtotal,
  addToCart,
  openCartSidebar,
  closeCartSidebar,
} from "./cart.js";
import {
  submitProduct,
  openModal,
  closeModal,
  closeModalOnOverlay,
} from "./modal.js";

export function init() {
  // Initialize state from URL or initialData
  const params = new URLSearchParams(window.location.search);
  state.currentPage =
    parseInt(params.get("pageNumber")) || window.initialData.currentPage;
  state.pageSize =
    parseInt(params.get("pageSize")) || window.initialData.pageSize;
  state.searchText = params.get("search") || window.initialData.searchText;
  state.totalResults = window.initialData.totalResults;

  // Set initial UI values
  const searchEl = document.getElementById("searchInput");
  if (searchEl) searchEl.value = state.searchText;
  const perPageBtn = document.getElementById("perPageBtn");
  if (perPageBtn) perPageBtn.textContent = `${state.pageSize} per page`;

  // Setup debounced search
  let debounceTimer;
  if (searchEl) {
    searchEl.addEventListener("keyup", () => {
      clearTimeout(debounceTimer);
      const value = searchEl.value.trim();
      debounceTimer = setTimeout(() => {
        if (value.length >= 2 || value.length === 0) {
          state.searchText = value;
          state.currentPage = 1;
          updateUrl();
          fetchData();
        }
      }, 300);
    });
  }

  // Setup slug generation
  const nameInput = document.getElementById("productName");
  if (nameInput) {
    nameInput.addEventListener("input", () => generateSlug());
  }

  // Bind all event listeners
  bindEventListeners();

  // Initial data fetch
  fetchData();
  bindPerDropdown();
  updateCartCount();
}

function bindEventListeners() {
  // Search and product management
  const searchBtn = document.getElementById("searchBtn");
  if (searchBtn) searchBtn.addEventListener("click", applyFilters);

  const addProductBtn = document.getElementById("addProductBtn");
  if (addProductBtn) addProductBtn.addEventListener("click", openModal);

  const perPageBtn = document.getElementById("perPageBtn");
  if (perPageBtn) perPageBtn.addEventListener("click", togglePerDropdown);

  const generateSlugBtn = document.getElementById("generateSlugBtn");
  if (generateSlugBtn) generateSlugBtn.addEventListener("click", generateSlug);

  const submitProductBtn = document.getElementById("submitProductBtn");
  if (submitProductBtn)
    submitProductBtn.addEventListener("click", submitProduct);

  // Pagination
  const prevBtn = document.getElementById("prevBtn");
  if (prevBtn) prevBtn.addEventListener("click", prevPage);

  const nextBtn = document.getElementById("nextBtn");
  if (nextBtn) nextBtn.addEventListener("click", nextPage);

  // Modal
  const modalOverlay = document.getElementById("modalOverlay");
  if (modalOverlay) modalOverlay.addEventListener("click", closeModalOnOverlay);

  const modalCloseBtn = document.getElementById("modalCloseBtn");
  if (modalCloseBtn) modalCloseBtn.addEventListener("click", closeModal);

  // Cart
  const cartButton = document.getElementById("cartButton");
  if (cartButton) cartButton.addEventListener("click", openCartSidebar);

  const cartCloseBtn = document.getElementById("cartCloseBtn");
  if (cartCloseBtn)
    cartCloseBtn.addEventListener("click", () => closeCartSidebar());

  const cartOverlay = document.getElementById("cartSidebarOverlay");
  if (cartOverlay)
    cartOverlay.addEventListener("click", (e) => closeCartSidebar(e));

  // Event delegation for dynamic elements
  document.addEventListener("click", handleDynamicEvents);
}

function handleDynamicEvents(e) {
  // Handle quantity buttons
  if (e.target.closest(".quantity-btn[data-action='increase']")) {
    increaseQuantity(e.target);
  } else if (e.target.closest(".quantity-btn[data-action='decrease']")) {
    decreaseQuantity(e.target);
  }

  // Handle add to cart buttons
  else if (e.target.closest(".confirm-add-btn[data-action='add-to-cart']")) {
    addToCart(e.target);
  }

  // Handle cart quantity buttons
  else if (e.target.closest(".qty-btn[data-action='cart-increase']")) {
    updateCartQuantity(e.target, 1);
  } else if (e.target.closest(".qty-btn[data-action='cart-decrease']")) {
    updateCartQuantity(e.target, -1);
  }

  // Handle page buttons
  else if (e.target.closest(".page-btn")) {
    const page = parseInt(e.target.dataset.page);
    if (!isNaN(page)) goToPage(page);
  }

  // Handle per page options
  else if (e.target.closest(".per-page-option")) {
    const value = parseInt(e.target.dataset.value);
    if (!isNaN(value)) changePerPage(value);
  }
}

// Helper functions for quantity adjustment
function increaseQuantity(btn) {
  const row = btn.closest(".quantity-row");
  if (!row) return;
  const display = row.querySelector(".quantity-display");
  if (display) {
    const current = parseInt(display.textContent) || 1;
    display.textContent = current + 1;
  }
}

function decreaseQuantity(btn) {
  const row = btn.closest(".quantity-row");
  if (!row) return;
  const display = row.querySelector(".quantity-display");
  if (display) {
    const current = parseInt(display.textContent) || 1;
    if (current > 1) display.textContent = current - 1;
  }
}
