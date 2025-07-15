(function () {
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
        loadCartPreview(); // 👈 This ensures the popup updates immediately
    }

    // Expose to global scope
    window.addToCart = addToCart;
})();
