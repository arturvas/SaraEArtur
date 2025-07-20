export const giftsData = {
    categories: [
        {
            id: 'cozinha',
            title: 'Cozinha',
            description: 'Ajude a montar os itens básicos da nossa cozinha!',
            items: [
                { id: 'panela', title: 'Jogo de Panelas', price: 500 },
                { id: 'liquidificador', title: 'Liquidificador', price: 300 },
                { id: 'copos', title: 'Conjunto de Copos', price: 150 },
            ],
        },
        {
            id: 'cama-banho',
            title: 'Cama & Banho',
            description: 'Conforto para o nosso dia a dia!',
            items: [
                { id: 'lencois', title: 'Lençóis 400 fios', price: 600 },
                { id: 'toalhas', title: 'Toalhas de Banho', price: 200 },
            ],
        },
        {
            id: 'valor-personalizado',
            title: 'Valor Personalizado',
            description: 'Contribua com qualquer valor e deixe uma mensagem especial!',
            items: null, // Especial
        },
    ],
};
