import { state } from "./state.js";
import { init } from "./init.js";
import * as utilities from "./utilities.js";
import * as products from "./products.js";
import * as cart from "./cart.js";
import * as modal from "./modal.js";

// Create global home object with all functions
window.home = {
  state,
  init,
  // Utilities
  generateSlug: utilities.generateSlug,
  togglePerDropdown: utilities.togglePerDropdown,
  // Products
  changePerPage: products.changePerPage,
  prevPage: products.prevPage,
  nextPage: products.nextPage,
  goToPage: products.goToPage,
  applyFilters: products.applyFilters,
  // Cart
  openCartSidebar: cart.openCartSidebar,
  closeCartSidebar: cart.closeCartSidebar,
  // Modal
  openModal: modal.openModal,
  closeModal: modal.closeModal,
  closeModalOnOverlay: modal.closeModalOnOverlay,
  submitProduct: modal.submitProduct,
};

// Initialize when DOM is ready
document.addEventListener("DOMContentLoaded", () => home.init());
