
document.addEventListener('DOMContentLoaded', function () {

    // ---------- Imagen de portada ----------
    const imageInput = document.getElementById('ImageFile');
    const dropzone = document.getElementById('imageDropzone');
    const previewImg = document.getElementById('imagePreview');
    const placeholder = document.getElementById('imagePlaceholder');

    if (imageInput && previewImg) {
        imageInput.addEventListener('change', function (e) {
            previewFile(e.target.files[0], previewImg, placeholder);
        });

        if (dropzone) {
            dropzone.addEventListener('click', () => imageInput.click());
            wireDragAndDrop(dropzone, imageInput);
        }
    }

    // ---------- Búsqueda en vivo (Index) ----------
    const searchInput = document.getElementById('cchSearch');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            const term = this.value.trim().toLowerCase();
            document.querySelectorAll('[data-search-item]').forEach(function (item) {
                const text = item.getAttribute('data-search-item').toLowerCase();
                item.style.display = text.includes(term) ? '' : 'none';
            });
        });
    }

    // ---------- Agregar bloque de color ----------
    const variantsContainer = document.getElementById('variantsContainer');
    const addVariantBtn = document.getElementById('addVariantBtn');
    const variantTemplate = document.getElementById('variantBlockTemplate');

    if (addVariantBtn && variantsContainer && variantTemplate) {
        addVariantBtn.addEventListener('click', function () {
            const nextIndex = variantsContainer.querySelectorAll('.cch-variant-block').length;
            const html = variantTemplate.innerHTML.replaceAll('__IDX__', nextIndex);
            const wrapper = document.createElement('div');
            wrapper.innerHTML = html.trim();
            const newBlock = wrapper.firstElementChild;

            // Le damos id único a cada checkbox/label recién clonado para que
            // la etiqueta también sea clicable (accesibilidad + evita clics perdidos)
            newBlock.querySelectorAll('.cch-size-check').forEach(function (chk, idx) {
                const uid = `chk_${nextIndex}_${idx}`;
                chk.id = uid;
                const label = chk.closest('.form-check').querySelector('.form-check-label');
                if (label) label.setAttribute('for', uid);
            });

            variantsContainer.appendChild(newBlock);
        });
    }

    // ---------- Agregar foto de galería ----------
    const galleryContainer = document.getElementById('galleryContainer');
    const addGalleryBtn = document.getElementById('addGalleryBtn');
    const galleryTemplate = document.getElementById('galleryBlockTemplate');

    if (addGalleryBtn && galleryContainer && galleryTemplate) {
        addGalleryBtn.addEventListener('click', function () {
            const nextIndex = galleryContainer.querySelectorAll('.cch-gallery-block').length;
            const html = galleryTemplate.innerHTML.replaceAll('__IDX__', nextIndex);
            const wrapper = document.createElement('div');
            wrapper.innerHTML = html.trim();
            galleryContainer.appendChild(wrapper.firstElementChild);
        });
    }

    // ---------- Delegación: quitar bloques y abrir selector de archivo ----------
    document.addEventListener('click', function (e) {
        const removeVariant = e.target.closest('.cch-remove-variant');
        if (removeVariant) {
            removeVariant.closest('.cch-variant-block').remove();
            renumberBlocks('#variantsContainer', '.cch-variant-block', 'Variants');
            return;
        }

        const removeGallery = e.target.closest('.cch-remove-gallery');
        if (removeGallery) {
            const block = removeGallery.closest('.cch-gallery-block');
            if (block.getAttribute('data-existing') === 'true') {
                block.querySelector('.cch-gallery-todelete').value = 'true';
                block.classList.add('d-none');
            } else {
                block.remove();
            }
            renumberBlocks('#galleryContainer', '.cch-gallery-block', 'GalleryImages');
            return;
        }

        const variantDropzone = e.target.closest('.cch-variant-dropzone');
        if (variantDropzone) {
            variantDropzone.parentElement.querySelector('.cch-variant-file')?.click();
            return;
        }

        const galleryDropzone = e.target.closest('.cch-gallery-dropzone');
        if (galleryDropzone) {
            galleryDropzone.parentElement.querySelector('.cch-gallery-file')?.click();
        }
    });

    // ---------- Delegación: checkboxes de talla y previews de imagen ----------
    document.addEventListener('change', function (e) {
        if (e.target.classList.contains('cch-size-check')) {
            const chip = e.target.closest('.cch-size-chip');
            const stockInput = chip.querySelector('.cch-size-stock');
            stockInput.disabled = !e.target.checked;
            chip.classList.toggle('cch-size-chip-active', e.target.checked);
            if (!e.target.checked) {
                stockInput.value = 0;
            } else if (!stockInput.value) {
                stockInput.value = 0;
            }
            return;
        }

        if (e.target.classList.contains('cch-variant-file')) {
            const row = e.target.closest('.cch-variant-image-row');
            previewFile(e.target.files[0], row.querySelector('.cch-variant-preview'), row.querySelector('[data-role="placeholder"]'));
            return;
        }

        if (e.target.classList.contains('cch-gallery-file')) {
            const block = e.target.closest('.cch-gallery-block');
            previewFile(e.target.files[0], block.querySelector('.cch-gallery-preview'), block.querySelector('[data-role="placeholder"]'));
            return;
        }

        if (e.target.classList.contains('cch-variant-color')) {
            const value = e.target.value;
            if (!value) return;

            let duplicated = false;
            document.querySelectorAll('.cch-variant-color').forEach(function (select) {
                if (select !== e.target && select.value === value) duplicated = true;
            });

            if (duplicated) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Color repetido',
                    text: 'Ese color ya fue agregado en otro bloque. Elige uno diferente o edita el bloque existente.'
                });
                e.target.value = '';
            }
        }
    });

    // ---------- Validación antes de enviar el formulario de producto ----------
    const productForm = document.getElementById('productForm');
    if (productForm) {
        productForm.addEventListener('submit', function (e) {
            const errors = [];

            document.querySelectorAll('.cch-variant-block').forEach(function (block, idx) {
                const colorSelect = block.querySelector('.cch-variant-color');
                const hasColor = colorSelect && colorSelect.value;
                const checkedSizes = block.querySelectorAll('.cch-size-check:checked');

                if (hasColor && checkedSizes.length === 0) {
                    errors.push(`El bloque de color #${idx + 1} no tiene ninguna talla marcada.`);
                }

                if (!hasColor && checkedSizes.length > 0) {
                    errors.push(`El bloque #${idx + 1} tiene tallas marcadas pero no se seleccionó un color.`);
                }
            });

            if (errors.length > 0) {
                e.preventDefault();
                Swal.fire({
                    icon: 'error',
                    title: 'Revisa los colores y tallas',
                    html: errors.map(err => `<div>${err}</div>`).join(''),
                    confirmButtonColor: '#141414'
                });
            }
        });
    }
});

function previewFile(file, imgEl, placeholderEl) {
    if (!file || !imgEl) return;
    const reader = new FileReader();
    reader.onload = function (ev) {
        imgEl.src = ev.target.result;
        imgEl.classList.remove('d-none');
        if (placeholderEl) placeholderEl.classList.add('d-none');
    };
    reader.readAsDataURL(file);
}

function wireDragAndDrop(dropzone, input) {
    dropzone.addEventListener('dragover', function (e) {
        e.preventDefault();
        dropzone.classList.add('cch-dropzone-active');
    });
    dropzone.addEventListener('dragleave', function () {
        dropzone.classList.remove('cch-dropzone-active');
    });
    dropzone.addEventListener('drop', function (e) {
        e.preventDefault();
        dropzone.classList.remove('cch-dropzone-active');
        if (e.dataTransfer.files.length) {
            input.files = e.dataTransfer.files;
            input.dispatchEvent(new Event('change'));
        }
    });
}

function renumberBlocks(containerSelector, blockSelector, prefix) {
    const blocks = document.querySelectorAll(`${containerSelector} ${blockSelector}`);
    const pattern = new RegExp(`${prefix}\\[\\d+\\]`);
    blocks.forEach(function (block, index) {
        block.setAttribute('data-index', index);
        block.querySelectorAll('[name]').forEach(function (el) {
            el.name = el.name.replace(pattern, `${prefix}[${index}]`);
        });
    });
}

// Confirmación de eliminación reutilizable
function cchConfirmDelete(url, itemName) {
    Swal.fire({
        title: `¿Eliminar ${itemName || 'este elemento'}?`,
        text: 'Esta acción no se puede deshacer.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#141414',
        cancelButtonColor: '#e63946',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = url;
        }
    });
}

/* ---------- TARJETA CLICABLE + PRECIO DE OFERTA ---------- */
.cch - product - link {
    display: block;
    color: inherit;
    text - decoration: none;
    cursor: pointer;
}

.cch - product - link: hover.cch - product - name {
    text - decoration: underline;
}

.cch - product - discount - badge {
    position: absolute;
    bottom: 10px;
    left: 10px;
    background: var(--cch - danger);
    color: #fff;
    font - weight: 700;
    font - size: .7rem;
    padding: .25rem .6rem;
    border - radius: 50px;
}

.cch - product - price - row {
    display: flex;
    align - items: baseline;
    gap: .5rem;
    margin - bottom: .3rem;
}

.cch - product - price - row.cch - product - price {
    margin - top: 0;
}