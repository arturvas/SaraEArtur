export type Gift = {
  id: string;
  title: string;
  price: number;
  paymentLink: string;
};

export type GiftCategory = {
  title: string;
  items: Gift[];
};

const stripePlaceholder = 'https://buy.stripe.com/your-link-here';

export const giftCategories: GiftCategory[] = [
  {
    title: 'Utensílios da Cozinha',
    items: [
      { id: 'kitchen-1', title: 'Kit copo e jarra', price: 120, paymentLink: stripePlaceholder },
      { id: 'kitchen-2', title: 'Kit refratárias', price: 130, paymentLink: stripePlaceholder },
      { id: 'kitchen-3', title: 'Jogo de xícaras', price: 142, paymentLink: stripePlaceholder },
      { id: 'kitchen-4', title: 'Escorregador de louça', price: 150, paymentLink: stripePlaceholder },
      { id: 'kitchen-5', title: 'Panela de pressão', price: 130, paymentLink: stripePlaceholder },
      { id: 'kitchen-6', title: 'Jogo de panelas', price: 400, paymentLink: stripePlaceholder },
    ],
  },
  {
    title: 'Eletroportáteis',
    items: [
      { id: 'electro-1', title: 'Liquidificador', price: 170, paymentLink: stripePlaceholder },
      { id: 'electro-2', title: 'Ferro de passar', price: 200, paymentLink: stripePlaceholder },
      { id: 'electro-3', title: 'Air Fryer', price: 542, paymentLink: stripePlaceholder },
      { id: 'electro-4', title: 'Microondas', price: 570, paymentLink: stripePlaceholder },
      { id: 'electro-5', title: 'Filtro de água', price: 650, paymentLink: stripePlaceholder },
      { id: 'electro-6', title: 'Batedeira', price: 250, paymentLink: stripePlaceholder },
    ],
  },
  {
    title: 'Cama & Banho',
    items: [
      { id: 'bed-1', title: 'Jogo de cama', price: 350, paymentLink: stripePlaceholder },
      { id: 'bed-2', title: 'Kit toalhas de banho', price: 150, paymentLink: stripePlaceholder },
      { id: 'bed-3', title: 'Par de travesseiros', price: 150, paymentLink: stripePlaceholder },
      { id: 'bed-4', title: 'Jogo de fronhas', price: 120, paymentLink: stripePlaceholder },
      { id: 'bed-5', title: 'Tapetes pro banheiro', price: 90, paymentLink: stripePlaceholder },
      { id: 'bed-6', title: 'Edredom de casal', price: 200, paymentLink: stripePlaceholder },
    ],
  },
  {
    title: 'Casa & Utilidades',
    items: [
      { id: 'home-1', title: 'Tapete', price: 199, paymentLink: stripePlaceholder },
      { id: 'home-2', title: 'Fogão', price: 900, paymentLink: stripePlaceholder },
      { id: 'home-3', title: 'Geladeira', price: 2000, paymentLink: stripePlaceholder },
    ],
  },
  {
    title: 'Mudança',
    items: [
      { id: 'moving-1', title: 'Primeiro passo', price: 100, paymentLink: stripePlaceholder },
      { id: 'moving-2', title: 'Mãos à obra', price: 300, paymentLink: stripePlaceholder },
      { id: 'moving-3', title: 'Novo lar', price: 500, paymentLink: stripePlaceholder },
    ],
  },
  {
    title: 'Lua de mel',
    items: [
      { id: 'honeymoon-1', title: 'Descanso merecido', price: 150, paymentLink: stripePlaceholder },
      { id: 'honeymoon-2', title: 'Viagem a dois', price: 400, paymentLink: stripePlaceholder },
      { id: 'honeymoon-3', title: 'Sonho realizado', price: 800, paymentLink: stripePlaceholder },
    ],
  },
];
