document.addEventListener("DOMContentLoaded", function () {
    const tables = document.querySelectorAll("table[data-paginacion='true']");

    tables.forEach((tableElement, tableIndex) => {
        let rowsPerPage = parseInt(tableElement.dataset.rowsPerPage) || 5;
        const tbody = tableElement.querySelector("tbody");
        const allRows = Array.from(tbody.querySelectorAll("tr"));
        const paginationId = tableElement.dataset.paginationId || `pagination-${tableIndex}`;
        const pagination = document.getElementById(paginationId);
        const tableId = tableElement.id;

        let filteredRows = [...allRows];
        let currentPage = 0;

        if (!pagination || allRows.length === 0) return;
        const selector = document.createElement("select");
        selector.className = "form-select form-select-sm w-auto mb-2";
        selector.innerHTML = [5, 10, 25, 50].map(n =>
            `<option value="${n}" ${n === rowsPerPage ? "selected" : ""}>${n} por página</option>`
        ).join("");
        tableElement.parentNode.insertBefore(selector, tableElement);

        selector.addEventListener("change", () => {
            rowsPerPage = parseInt(selector.value);
            currentPage = 0;
            displayRows(currentPage);
            setupPagination();
        });
        
        const fechaInicioInput = document.getElementById("fechaInicio");
        const fechaFinInput = document.getElementById("fechaFin");
        
        if (fechaInicioInput && fechaFinInput) {
            const limpiarBtn = document.createElement("button");
            limpiarBtn.className = "btn btn-secondary mt-2";
            limpiarBtn.textContent = "Limpiar filtro";
            limpiarBtn.type = "button";
            fechaFinInput.parentElement.appendChild(limpiarBtn);

            fechaInicioInput.addEventListener("change", aplicarFiltros);
            fechaFinInput.addEventListener("change", aplicarFiltros);
            limpiarBtn.addEventListener("click", () => {
                fechaInicioInput.value = "";
                fechaFinInput.value = "";
                aplicarFiltros();
            });
        }

        function aplicarFiltros() {
            const inicio = fechaInicioInput?.value ? new Date(fechaInicioInput.value) : null;
            const fin = fechaFinInput?.value ? new Date(fechaFinInput.value) : null;

            filteredRows = allRows.filter(row => {
                const fechaTexto = row.cells[1]?.textContent.trim();
                const [dia, mes, año] = fechaTexto.split("/");
                const fechaRow = new Date(`${año}-${mes}-${dia}`);

                if (inicio && fechaRow < inicio) return false;
                if (fin && fechaRow > fin) return false;

                return true;
            });

            currentPage = 0;
            displayRows(currentPage);
            setupPagination();
        }

        function displayRows(page) {
            allRows.forEach(row => row.style.display = "none");
            const start = page * rowsPerPage;
            const end = start + rowsPerPage;
            filteredRows.slice(start, end).forEach(row => {
                row.style.display = "";
            });
        }

        function setupPagination() {
            pagination.innerHTML = "";
            const pageCount = Math.ceil(filteredRows.length / rowsPerPage);
            if (pageCount <= 1) return;

            for (let i = 0; i < pageCount; i++) {
                const li = document.createElement("li");
                li.classList.add("page-item");
                li.innerHTML = `<a class="page-link" href="#">${i + 1}</a>`;
                li.addEventListener("click", (e) => {
                    e.preventDefault();
                    currentPage = i;
                    displayRows(currentPage);
                    pagination.querySelectorAll("li").forEach(p => p.classList.remove("active"));
                    li.classList.add("active");
                });
                pagination.appendChild(li);
            }

            if (pagination.firstChild) {
                pagination.firstChild.classList.add("active");
            }
        }
        
        aplicarFiltros();
    });
});
