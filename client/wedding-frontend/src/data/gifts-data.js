export const giftsData = {
    categories: [
        {
            id: 'utensilios-cozinha',
            title: 'Utensílios da Cozinha',
            description: 'Pequenos detalhes que fazem toda a diferença.',
            items: [
                { id: 'kit-copo-jarra', title: 'Kit copo e jarra', price: 120 },
                { id: 'travessa', title: 'Travessa de vidro', price: 80 },
                { id: 'jg-xicaras', title: 'Jogo de xícaras', price: 142 },
                { id: 'escorredor', title: 'Escorredor de louça', price: 150 },
                { id: 'panela-pressao', title: 'Panela de pressão', price: 130 },
                { id: 'jg-panelas', title: 'Jogo de panelas', price: 400 },
            ],
        },
        {
            id: 'eletroportateis',
            title: 'Eletroportáteis',
            description: 'Facilidade e praticidade no nosso dia a dia.',
            items: [
                { id: 'liquidificador', title: 'Liquidificador', price: 170 },
                { id: 'ferro-passar', title: 'Ferro de passar', price: 200 },
                { id: 'air-fryer', title: 'Air Fryer', price: 542 },
                { id: 'microondas', title: 'Microondas', price: 570 },
                { id: 'filtro-agua', title: 'Filtro de água', price: 650 },
                { id: 'batedeira', title: 'Batedeira', price: 250 },
            ],
        },
        {
            id: 'cama-banho',
            title: 'Cama & Banho',
            description: 'Conforto no momento de descanso.',
            items: [
                { id: 'jg-cama', title: 'Jogo de cama', price: 350 },
                { id: 'toalhas', title: 'Toalhas de Banho', price: 90 },
                { id: 'travesseiros', title: 'Par de travesseiros', price: 150 },
                { id: 'jg-fronhas', title: 'Jogo de fronhas', price: 70 },
                { id: 'tapete-banheiro', title: 'Tapete pro banheiro', price: 50 },
                { id: 'edredom', title: 'Edredom de casal', price: 200 },
            ],
        },
        {
            id: 'casa-utilidades',
            title: 'Casa & Utilidades',
            description: 'Essencial para um lar completo.',
            items: [
                { id: 'jg-tapete', title: 'Jogo de tapetes', price: 99 },
                { id: 'fogao', title: 'Fogão', price: 900 },
                { id: 'geladeira', title: 'Geladeira', price: 2000 },
            ],
        },
        {
            id: 'mudanca',
            title: 'Mudança',
            isCustom: true,
            description: 'Para começarmos bem na casa nova.',
            items: [
                { id: 'mudanca-basica', title: 'Primeiro passo', price: 100 },
                { id: 'mudanca-media', title: 'Mãos à obra', price: 300 },
                { id: 'mudanca-completa', title: 'Novo lar', price: 500 },
            ],
        },
        {
            id: 'lua-de-mel',
            title: 'Lua de mel',
            isCustom: true,
            description: 'Momentos inesquecíveis na nossa primeira viagem.',
            items: [
                { id: 'lua-mel-basica', title: 'Descanso merecido', price: 150 },
                { id: 'lua-mel-media', title: 'Viagem a dois', price: 400 },
                { id: 'lua-mel-completa', title: 'Sonho realizado', price: 800 },
            ],
        },
        {
            id: 'valor-personalizado',
            title: 'Valor Personalizado',
            description: 'Fique à vontade para escolher o valor do presente.',
            items: null, // Especial
        },
    ],
};
