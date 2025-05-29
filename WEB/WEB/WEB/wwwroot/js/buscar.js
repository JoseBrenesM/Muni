document.getElementById("busquedaInput").addEventListener("input", function () {
    const texto = this.value.toLowerCase();
    const filas = document.querySelectorAll("table tbody tr");

    filas.forEach(fila => {
        const contenido = fila.textContent.toLowerCase();
        fila.style.display = contenido.includes(texto) ? "" : "none";
    });
});