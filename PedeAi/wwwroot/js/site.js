const CART_KEY = "pedeai_cart";

function getCart() {
    const cart = localStorage.getItem(CART_KEY);
    return cart ? JSON.parse(cart) : { items: [] };
}

function saveCart(cart) {
    localStorage.setItem(CART_KEY, JSON.stringify(cart));
}

function addToCart(produto) {
    const cart = getCart();
    const index = cart.items.findIndex(item => item.produtoId === produto.produtoId);

    if (index !== -1) {
        cart.items[index].quantidade += 1;
    } else {
        cart.items.push({ ...produto, quantidade: 1 });
    }

    saveCart(cart);
    if (typeof window.loadCartPreview === "function") {
        window.loadCartPreview(); // 🟢 Automatically update popup
    }
}

// Expose globally
window.addToCart = addToCart;
window.getCart = getCart;
window.saveCart = saveCart;

document.addEventListener("DOMContentLoaded", () => {
    const cartIcon = document.getElementById("cart-icon");
    const cartPopup = document.getElementById("cart-popup");

    if (!cartIcon || !cartPopup) return;

    const cartWrapper = cartIcon.closest(".cart-wrapper");
    const isMobile = window.matchMedia("(hover: none)").matches;

    window.loadCartPreview?.();

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