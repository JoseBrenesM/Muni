document.addEventListener("DOMContentLoaded", function () {
    const tabla = document.getElementById("tabla-horarios");
    const filas = Array.from(tabla?.querySelectorAll("tbody tr") || []);
    const paginacion = document.getElementById("pagination");
    const fechaInicioInput = document.getElementById("fechaInicio");
    const fechaFinInput = document.getElementById("fechaFin");
    const btnLimpiar = document.getElementById("btnLimpiarFiltro");
    const rowsPerPageSelect = document.getElementById("rowsPerPage");

    function getRowsPerPage() {
        return parseInt(rowsPerPageSelect?.value || "5");
    }

    function aplicarFiltros() {
        const fechaInicio = new Date(fechaInicioInput.value);
        const fechaFin = new Date(fechaFinInput.value);
        const tieneInicio = !isNaN(fechaInicio);
        const tieneFin = !isNaN(fechaFin);

        return filas.filter(row => {
            const fechaTexto = row.cells[0]?.textContent.trim();
            if (!fechaTexto || !fechaTexto.includes("/")) return false;

            const [dia, mes, año] = fechaTexto.split("/");
            const fechaRow = new Date(parseInt(año), parseInt(mes) - 1, parseInt(dia));

            if (isNaN(fechaRow)) return false;

            if (tieneInicio && fechaRow < fechaInicio) return false;
            if (tieneFin && fechaRow > fechaFin) return false;

            return true;
        });
    }

    function mostrarPagina(pagina, datos) {
        const rowsPerPage = getRowsPerPage();
        const inicio = (pagina - 1) * rowsPerPage;
        const fin = inicio + rowsPerPage;
        const datosPagina = datos.slice(inicio, fin);

        filas.forEach(fila => fila.style.display = "none");
        datosPagina.forEach(fila => fila.style.display = "");

        generarBotonesPaginacion(datos.length, pagina);
    }

    function generarBotonesPaginacion(totalFilas, paginaActual) {
        const rowsPerPage = getRowsPerPage();
        paginacion.innerHTML = "";
        const totalPaginas = Math.ceil(totalFilas / rowsPerPage);

        for (let i = 1; i <= totalPaginas; i++) {
            const btn = document.createElement("li");
            btn.className = `page-item ${i === paginaActual ? "active" : ""}`;
            btn.innerHTML = `<a class="page-link" href="#">${i}</a>`;
            btn.addEventListener("click", e => {
                e.preventDefault();
                mostrarPagina(i, aplicarFiltros());
            });
            paginacion.appendChild(btn);
        }
    }
    
    [fechaInicioInput, fechaFinInput].forEach(input => {
        input.addEventListener("change", () => mostrarPagina(1, aplicarFiltros()));
    });
    rowsPerPageSelect?.addEventListener("change", () => {
        mostrarPagina(1, aplicarFiltros());
    });
    btnLimpiar?.addEventListener("click", function () {
        fechaInicioInput.value = "";
        fechaFinInput.value = "";
        mostrarPagina(1, filas);
    });
    
    if (tabla) {
        mostrarPagina(1, aplicarFiltros());
    }
});
