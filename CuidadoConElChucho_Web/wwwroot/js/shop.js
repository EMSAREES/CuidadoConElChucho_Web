
document.addEventListener('DOMContentLoaded', function () {

    const mainImg = document.getElementById('galleryMainImg');
    const thumbs = document.querySelectorAll('.cchs-thumb');
    const swatches = document.querySelectorAll('.cchs-swatch');
    const sizeGroups = document.querySelectorAll('.cchs-size-group');
    const sizesPlaceholder = document.getElementById('sizesPlaceholder');
    const selectedColorName = document.getElementById('selectedColorName');
    const addToCartBtn = document.getElementById('addToCartBtn');
    const skuDisplay = document.getElementById('skuDisplay');

    let selectedColorId = null;
    let selectedSizeId = null;

    // ---------- Miniaturas ----------
    thumbs.forEach(function (thumb) {
        thumb.addEventListener('click', function () {
            thumbs.forEach(t => t.classList.remove('cchs-thumb-active'));
            thumb.classList.add('cchs-thumb-active');
            if (mainImg) {
                mainImg.src = thumb.getAttribute('data-src');
            }
        });
    });

    // ---------- Selección de color ----------
    swatches.forEach(function (swatch) {
        swatch.addEventListener('click', function () {
            swatches.forEach(s => s.classList.remove('cchs-swatch-active'));
            swatch.classList.add('cchs-swatch-active');

            selectedColorId = swatch.getAttribute('data-color-id');
            selectedSizeId = null;

            if (selectedColorName) {
                selectedColorName.textContent = swatch.getAttribute('data-color-name');
            }

            const image = swatch.getAttribute('data-image');
            if (image && mainImg) {
                mainImg.src = image;
            }

            if (sizesPlaceholder) sizesPlaceholder.classList.add('d-none');

            sizeGroups.forEach(function (group) {
                const isMatch = group.getAttribute('data-color-id') === selectedColorId;
                group.classList.toggle('d-none', !isMatch);
                group.querySelectorAll('.cchs-size-btn').forEach(btn => btn.classList.remove('cchs-size-btn-active'));
            });

            updateAddToCartState();
        });
    });

    // ---------- Selección de talla ----------
    document.querySelectorAll('.cchs-size-btn').forEach(function (btn) {
        btn.addEventListener('click', function () {
            if (btn.disabled) return;

            document.querySelectorAll('.cchs-size-btn').forEach(b => b.classList.remove('cchs-size-btn-active'));
            btn.classList.add('cchs-size-btn-active');

            selectedSizeId = btn.getAttribute('data-size-id');

            if (skuDisplay) {
                skuDisplay.textContent = `SKU ${btn.getAttribute('data-sku')}`;
            }

            updateAddToCartState();
        });
    });

    function updateAddToCartState() {
        if (!addToCartBtn) return;

        if (selectedColorId && selectedSizeId) {
            addToCartBtn.disabled = false;
            addToCartBtn.classList.add('cchs-ready');
            addToCartBtn.textContent = 'AGREGAR AL CARRITO';
        } else if (selectedColorId) {
            addToCartBtn.disabled = true;
            addToCartBtn.classList.remove('cchs-ready');
            addToCartBtn.textContent = 'SELECCIONA UNA TALLA';
        } else {
            addToCartBtn.disabled = true;
            addToCartBtn.classList.remove('cchs-ready');
            addToCartBtn.textContent = 'SELECCIONA COLOR Y TALLA';
        }
    }

    if (addToCartBtn) {
        addToCartBtn.addEventListener('click', function () {
            if (addToCartBtn.disabled) return;

            // El carrito de compras es un módulo aparte todavía no implementado.
            Swal.fire({
                icon: 'info',
                title: '¡Casi listo!',
                text: 'El carrito de compras estará disponible pronto.',
                confirmButtonColor: '#141414'
            });
        });
    }
});