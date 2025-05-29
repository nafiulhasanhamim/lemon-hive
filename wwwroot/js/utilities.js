import { state } from "./state.js";

export function updateUrl() {
  const params = new URLSearchParams();
  params.set("pageNumber", state.currentPage);
  params.set("pageSize", state.pageSize);
  if (state.searchText) params.set("search", state.searchText);
  const newUrl = `${window.location.pathname}?${params.toString()}`;
  history.replaceState(null, "", newUrl);
}

export function generateSlug() {
  const nameEl = document.getElementById("productName");
  const slugEl = document.getElementById("productSlug");
  if (!nameEl || !slugEl) return;
  const slug = nameEl.value
    .toLowerCase()
    .replace(/[^a-z0-9\s-]/g, "")
    .trim()
    .replace(/\s+/g, "-");
  slugEl.value = slug;
}

export function showToast(message, type) {
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
}

export function bindPerDropdown() {
  document.addEventListener("click", (e) => {
    if (!e.target.closest(".per-page-select")) hidePerDropdown();
  });
}

export function togglePerDropdown() {
  document.getElementById("perDropdown").classList.toggle("active");
}

export function hidePerDropdown() {
  document.getElementById("perDropdown").classList.remove("active");
}

export function increaseQuantity(btn) {
  const row = btn.closest(".quantity-row");
  const display = row.querySelector(".quantity-display");
  display.textContent = parseInt(display.textContent) + 1;
}

export function decreaseQuantity(btn) {
  const row = btn.closest(".quantity-row");
  const display = row.querySelector(".quantity-display");
  let qty = parseInt(display.textContent);
  if (qty > 1) display.textContent = qty - 1;
}
