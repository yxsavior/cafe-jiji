async function carregarDashboard() {
    try {
        const res = await fetch(`${API_BASE}/pedidos/dashboard`, { headers: getHeaders() });
        if (res.status === 401 || res.status === 403) return logout();
        if (!res.ok) throw new Error("Erro na API");

        const dados = await res.json();

        console.log("Dados vindos da API:", dados);

        // 1. Atualiza Card 1: Faturamento Mensal
        const sinalCrescimento = dados.percentualCrescimento >= 0 ? "+" : "";
        document.getElementById("cardFaturamentoMensal").innerHTML = `R$ ${dados.faturamentoMensal.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}`;
        // Atualiza a legenda do subtexto do crescimento
        document.getElementById("subtextoFaturamento").innerHTML = `✨ ${sinalCrescimento}${dados.percentualCrescimento}% em relação ao mês anterior`;

        // 2. Atualiza Card 2: Pedidos Concluídos
        document.getElementById("cardPedidosConcluidos").innerText = dados.pedidosConcluidos;
        document.getElementById("subtextoPedidos").innerHTML = `☕ Média de ${dados.mediaClientesDiarios} clientes diários`;

        // 3. Atualiza Card 3: Taxas do Gatil
        document.getElementById("cardTaxasGatil").innerText = `R$ ${dados.taxasGatil.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}`;
        document.getElementById("subtextoGatil").innerHTML = `🐈 ${dados.visitasAgendadas} visitas agendadas`;

        // ==========================================
        // ATUALIZAÇÃO DOS GRÁFICOS COM DADOS REAIS
        // ==========================================
        
        // Gráfico de Linha Semanal
        if (chartFaturamento) chartFaturamento.destroy();
        const ctxFaturamento = document.getElementById('graficoFaturamento').getContext('2d');
        
        // Gera labels dos últimos 7 dias dinamicamente (ex: "20/05", "21/05"...)
        const labelsDias = Array.from({length: 7}, (_, i) => {
            const d = new Date();
            d.setDate(d.getDate() - (6 - i));
            return d.toLocaleDateString('pt-BR', { day: '2-digit', month: '2-digit' });
        });

        chartFaturamento = new Chart(ctxFaturamento, {
            type: 'line',
            data: {
                labels: labelsDias,
                datasets: [{
                    label: 'Faturamento Semanal (R$)',
                    data: dados.faturamentoSemanal, // Dados vindos do C#
                    borderColor: '#5d4037',
                    backgroundColor: 'rgba(93, 64, 55, 0.1)',
                    tension: 0.3,
                    fill: true
                }]
            },
            options: { responsive: true, maintainAspectRatio: false }
        });

        // Gráfico Donut de Categorias
        if (chartCategorias) chartCategorias.destroy();
        const ctxCategorias = document.getElementById('graficoCategorias').getContext('2d');
        
        const labelsCategorias = dados.categoriasMaisVendidas.map(c => c.categoria);
        const volumesCategorias = dados.categoriasMaisVendidas.map(c => c.quantidade);

        chartCategorias = new Chart(ctxCategorias, {
            type: 'doughnut',
            data: {
                labels: labelsCategorias.length ? labelsCategorias : ['Sem vendas'],
                datasets: [{
                    data: volumesCategorias.length ? volumesCategorias : [1],
                    backgroundColor: ['#5d4037', '#cec4ca', '#8d6e63', '#d7ccc8']
                }]
            },
            options: { responsive: true, maintainAspectRatio: false }
        });

    } catch (err) {
        console.error("Erro ao atualizar os dados dinâmicos do dashboard:", err);
    }
}