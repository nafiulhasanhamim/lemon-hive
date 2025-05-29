import { state } from "./state.js";
import { updateUrl, hidePerDropdown } from "./utilities.js";

export function fetchData() {
  const { currentPage, pageSize, searchText } = state;
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
      state.totalResults = data.totalResults;
      renderProducts(data.items);
      renderPagination();
    });
}

// in your renderer module (e.g. products.js)

export function renderProducts(items) {
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
             <span class="original-price">
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
                <img src="https://images.unsplash.com/photo-1473968512647-3e447244af8f?w=400&h=300&fit=crop&crop=center" alt="Product" class="product-image">          

        <div class="product-content">
          <div class="product-name">${p.productName}</div>
          <div class="product-price">${priceHtml}</div>
        </div>
        <div class="cart-controls">
          <!-- always-visible add-to-cart -->
          <button class="add-to-cart" data-action="add-to-cart">
            Add to Cart
          </button>
          <!-- hover-only quantity + confirm -->
          <div class="quantity-controls">
            <div class="quantity-row">
              <button class="quantity-btn" data-action="decrease">-</button>
              <span class="quantity-display">1</span>
              <button class="quantity-btn" data-action="increase">+</button>
            </div>
            <button class="confirm-add-btn" data-action="add-to-cart">
              Confirm
            </button>
          </div>
        </div>
      </div>`;
    })
    .join("");

  document.getElementById("resultsInfo").textContent = `Showing ${
    (state.currentPage - 1) * state.pageSize + 1
  }–${Math.min(state.currentPage * state.pageSize, state.totalResults)} of ${
    state.totalResults
  } results`;
}

export function renderPagination() {
  const container = document.getElementById("pageButtons");
  const totalPages = Math.ceil(state.totalResults / state.pageSize);
  let html = "";

  for (let i = 1; i <= totalPages; i++) {
    if (i === 6 && totalPages > 7) {
      html += '<span class="dots">…</span>';
      i = totalPages - 2;
    }
    html += `<button class="page-btn ${
      i === state.currentPage ? "active" : ""
    }" data-page="${i}">${i}</button>`;
  }
  container.innerHTML = html;
}

export function changePerPage(v) {
  state.pageSize = v;
  document.getElementById("perPageBtn").textContent = `${v} per page`;
  hidePerDropdown();
  state.currentPage = 1;
  updateUrl();
  fetchData();
}

export function prevPage() {
  if (state.currentPage > 1) {
    state.currentPage--;
    updateUrl();
    fetchData();
  }
}

export function nextPage() {
  const maxPage = Math.ceil(state.totalResults / state.pageSize);
  if (state.currentPage < maxPage) {
    state.currentPage++;
    updateUrl();
    fetchData();
  }
}

export function goToPage(n) {
  state.currentPage = n;
  updateUrl();
  fetchData();
}

export function applyFilters() {
  const searchEl = document.getElementById("searchInput");
  const value = searchEl.value.trim();
  state.searchText = value;
  state.currentPage = 1;
  updateUrl();
  fetchData();
}
