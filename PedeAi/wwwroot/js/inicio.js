

document.addEventListener('DOMContentLoaded', () => {
    const container = document.getElementById('restaurante-carousel');
    let page = 1;
    let loading = false;

    function showSkeletons(count = 5) {
        for (let i = 0; i < count; i++) {
            const skeleton = document.createElement('div');
            skeleton.className = 'skeleton-card';
            container.appendChild(skeleton);
        }
    }

    function removeSkeletons() {
        document.querySelectorAll('.skeleton-card').forEach(s => s.remove());
    }

    async function loadItems() {
        loading = true;

        showSkeletons(5);

        const newItems = await fetch(`/api/restaurante?page=${page}`)
            .then(res => res.json());

        removeSkeletons();

        newItems.forEach(restaurante => {
            const card = document.createElement('div');
            card.className = 'restaurant-card';
            card.innerHTML = `
            <img src="/${restaurante.logoPath}" alt="${restaurante.nome}" />
            <p>${restaurante.nome}</p>
        `;

            card.addEventListener('click', () => {
                const form = document.createElement('form');
                form.method = 'POST';
                form.action = '/RestauranteProdutos';

                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = 'restauranteId';
                input.value = restaurante.id;

                form.appendChild(input);

                const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');

                if (tokenInput) {
                    const clonedTokenInput = document.createElement('input');
                    clonedTokenInput.type = 'hidden';
                    clonedTokenInput.name = '__RequestVerificationToken';
                    clonedTokenInput.value = tokenInput.value;
                    form.appendChild(clonedTokenInput);
                }

                document.body.appendChild(form);
                form.submit();
            });

            container.appendChild(card);
        });

        page++;
        loading = false;
    }


    // Load first batch
    loadItems();

    // Infinite scroll handler
    container.addEventListener('scroll', () => {
        const nearEnd = container.scrollLeft + container.clientWidth >= container.scrollWidth - 100;
        if (nearEnd && !loading) {
            loadItems();
        }
    });
});