document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('input[type="password"]').forEach(input => {

        if (input.dataset.processed === "true") return;
        
        const wrapper = document.createElement("div");
        wrapper.style.position = "relative";
        wrapper.classList.add("password-wrapper");
        
        input.dataset.processed = "true";
        
        input.parentNode.insertBefore(wrapper, input);
        wrapper.appendChild(input);
        
        input.style.paddingRight = "40px";
        
        const toggleBtn = document.createElement("button");
        toggleBtn.type = "button";
        toggleBtn.innerHTML = '<i class="bi bi-eye"></i>';
        toggleBtn.style.position = "absolute";
        toggleBtn.style.top = "50%";
        toggleBtn.style.right = "10px";
        toggleBtn.style.transform = "translateY(-50%)";
        toggleBtn.style.border = "none";
        toggleBtn.style.background = "transparent";
        toggleBtn.style.cursor = "pointer";
        toggleBtn.style.zIndex = "2";
        toggleBtn.setAttribute("aria-label", "Mostrar/ocultar contraseña");

        wrapper.appendChild(toggleBtn);
        
        toggleBtn.addEventListener("click", () => {
            const isPassword = input.type === "password";
            input.type = isPassword ? "text" : "password";
            toggleBtn.innerHTML = isPassword
                ? '<i class="bi bi-eye-slash"></i>'
                : '<i class="bi bi-eye"></i>';
        });
    });
});
