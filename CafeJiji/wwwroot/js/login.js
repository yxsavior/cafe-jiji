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

        // 1. Linha de comando temporária para você inspecionar o que a API está cuspindo
        console.log("Dados retornados pela API no Login:", data);

        // 2. Garante que salvamos o token em todas as variações que suas telas pedem
        localStorage.setItem("token", data.tokenJWT);
        localStorage.setItem("token_jiji", data.tokenJWT); 

        // 3. Verifica o nome exato do campo de perfil que vem da API
        // (Se sua API retornar 'role' em vez de 'perfil', mude para data.role)
        const perfilUsuario = data.perfil || data.role || "";
        localStorage.setItem("role", perfilUsuario);

        // 4. Redirecionamento com tratamento de erro
        if (perfilUsuario.toLowerCase() === "gerente") {
            console.log("Redirecionando para o Painel do Gerente...");
            window.location.href = "gerente.html"; // Certifique-se de que o nome do arquivo é este mesmo
        } else if (perfilUsuario.toLowerCase() === "atendente") {
            console.log("Redirecionando para o Atendimento...");
            window.location.href = "atendimento.html";
        } else {
            console.log("Redirecionando para a Cozinha...");
            window.location.href = "cozinha.html";
        }

} catch (err) {
    // Isso vai travar a tela e te mostrar o erro real em um pop-up na tela
    alert("Ocorreu um erro no Login: " + err.message);
    
    errorBox.textContent = err.message === "Failed to fetch" 
      ? "Não foi possível conectar ao servidor. O Back-end está ligado?" 
      : err.message;
      
    errorBox.classList.remove("d-none");
  }
});