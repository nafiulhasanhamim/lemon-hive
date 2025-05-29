import { state } from "./state.js";
import { showToast } from "./utilities.js";
import { fetchData } from "./products.js";

export function submitProduct() {
  const f = document.getElementById("addProductForm");
  if (!f) return;

  const errContainer = document.getElementById("formErrors");
  if (errContainer) errContainer.innerHTML = "";

  const makeDateTime = (dateString) => {
    const today = new Date();
    const selected = new Date(dateString);
    const isToday = selected.toDateString() === today.toDateString();

    if (isToday) {
      return new Date(Date.now() + 60_000);
    } else {
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

  if (
    !data.productName ||
    !data.slug ||
    !data.productImage ||
    isNaN(data.price)
  ) {
    return showToast("Please fill in all fields correctly", "error");
  }

  fetch("/api/Products", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  })
    .then(async (res) => {
      const body = await res.json();
      if (res.ok) {
        showToast("Product added successfully!", "success");
        closeModal();
        state.currentPage = 1;
        fetchData();
      } else {
        if (body.errors && Array.isArray(body.errors)) {
          const msgs = body.errors.flatMap((e) =>
            e.errors.map(
              (msg) => `<li><strong>${e.field}:</strong> ${msg}</li>`
            )
          );
          if (errContainer) {
            errContainer.innerHTML = `<ul style="margin:0; padding-left:1.25rem;">${msgs.join(
              ""
            )}</ul>`;
          }
        } else if (errContainer) {
          errContainer.textContent = body.message || "Failed to add product.";
        }
      }
    })
    .catch((err) => {
      console.error(err);
      showToast("Network error while adding product", "error");
    });
}

export function openModal() {
  const overlay = document.getElementById("modalOverlay");
  if (overlay) {
    overlay.classList.add("active");
    document.body.style.overflow = "hidden";
  }
}

export function closeModal() {
  const overlay = document.getElementById("modalOverlay");
  if (overlay) {
    overlay.classList.remove("active");
    document.body.style.overflow = "auto";
  }
  const form = document.getElementById("addProductForm");
  if (form) form.reset();
}

export function closeModalOnOverlay(e) {
  if (e.target === e.currentTarget) closeModal();
}
