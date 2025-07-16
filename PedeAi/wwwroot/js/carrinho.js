/*!
 * Script Name: carrinho.js
 * Description: Handles shopping cart behavior for the PedeAí app
 * Author: Eduardo Matias
 * Created: 2025-07-16
 * Version: 1.0
 */

const CART_KEY = "pedeai_cart";

export function loadCartPreview() {
    const cart = getCart();
    const ul = document.getElementById("cart-popup-items");
    const totalEl = document.getElementById("cart-popup-total");
    if (!ul || !totalEl) return;

    ul.innerHTML = ''; // Clear current items

    let total = 0;

    cart.items.forEach(item => {
        const li = document.createElement('li');
        li.className = "d-flex justify-content-between align-items-center mb-1";

        const itemText = document.createElement('span');
        itemText.textContent = `${item.nome} x${item.quantidade} - R$ ${(item.preco * item.quantidade).toFixed(2)}`;

        const removeBtn = document.createElement('button');
        removeBtn.textContent = '✕';
        removeBtn.className = 'btn btn-sm btn-danger btn-remove-item';
        removeBtn.onclick = () => removeFromCart(item.produtoId);

        li.appendChild(itemText);
        li.appendChild(removeBtn);
        ul.appendChild(li);

        total += item.preco * item.quantidade;
    });

    totalEl.textContent = `R$ ${total.toFixed(2)}`;
}

export function removeFromCart(produtoId) {
    const cart = getCart();
    const index = cart.items.findIndex(item => item.produtoId === produtoId);

    if (index !== -1) {
        cart.items.splice(index, 1); // Remove the item
        saveCart(cart);
        loadCartPreview(); // Refresh popup
    }
}

export function getCart() {
    const cart = localStorage.getItem(CART_KEY);
    return cart ? JSON.parse(cart) : { items: [] };
}

export function saveCart(cart) {
    localStorage.setItem(CART_KEY, JSON.stringify(cart));
}

export function addToCart(produto) {
    const cart = getCart();
    const index = cart.items.findIndex(item => item.produtoId === produto.produtoId);

    if (index !== -1) {
        cart.items[index].quantidade += 1;
    } else {
        cart.items.push({ ...produto, quantidade: 1 });
    }

    saveCart(cart);

    if (typeof loadCartPreview === "function") {
        loadCartPreview(); // Call from same module or importing context
    }
}

export function initCartUI() {
    document.addEventListener("DOMContentLoaded", () => {
        const cartIcon = document.getElementById("cart-icon");
        const cartPopup = document.getElementById("cart-popup");

        if (!cartIcon || !cartPopup) return;

        const cartWrapper = cartIcon.closest(".cart-wrapper");
        const isMobile = window.matchMedia("(hover: none)").matches;

        if (typeof loadCartPreview === "function") {
            loadCartPreview();
        }

        if (isMobile) {
            cartIcon.addEventListener("click", (e) => {
                e.preventDefault();
                cartPopup.classList.toggle("show");
            });

            document.addEventListener("click", (e) => {
                if (!cartWrapper.contains(e.target)) {
                    cartPopup.classList.remove("show");
                }
            });
        } else {
            let hideTimeout;

            cartWrapper.addEventListener("mouseenter", () => {
                clearTimeout(hideTimeout);
                cartPopup.classList.add("show");
            });

            cartWrapper.addEventListener("mouseleave", () => {
                hideTimeout = setTimeout(() => cartPopup.classList.remove("show"), 150);
            });
        }
    });
}
