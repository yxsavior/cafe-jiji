document.getElementById("loginForm").addEventListener("submit", async (e) => {
  e.preventDefault();

  const errorBox = document.getElementById("error");
  errorBox.classList.add("d-none");

  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  try {
    // ATENÇÃO: Confirme se a sua API está realmente usando a porta 5120
    const response = await fetch("http://localhost:5120/api/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        username: username,
        senha: password
      })
    });

    if (!response.ok) {
      const errorData = await response.json().catch(() => ({}));
      throw new Error(errorData.mensagem || "Credenciais inválidas");
    }

    const data = await response.json();

    // Salva os dados da sessão
    localStorage.setItem("token", data.tokenJWT);
    localStorage.setItem("role", data.perfil);

    // Redireciona para a tela correta
    window.location.href = "atendimento.html";

  } catch (err) {
    // Se cair aqui com "Failed to fetch", a API está desligada ou na porta errada
    errorBox.textContent = err.message === "Failed to fetch" 
      ? "Não foi possível conectar ao servidor. O Back-end está ligado?" 
      : err.message;
      
    errorBox.classList.remove("d-none");
  }
});