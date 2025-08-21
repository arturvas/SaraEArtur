export type NavItem = {
  id: string;
  label: string;
  icon: string;
};

export const navItems: NavItem[] = [
  { id: 'inicio', label: 'Início', icon: 'lucide:heart' },
  { id: 'detalhes', label: 'Detalhes', icon: 'lucide:calendar-heart' },
  { id: 'dress-code', label: 'Traje', icon: 'lucide:shirt' },
  { id: 'historia', label: 'História', icon: 'lucide:book-open' },
  { id: 'galeria', label: 'Galeria', icon: 'lucide:camera' },
  { id: 'presentes', label: 'Presentes', icon: 'lucide:gift' },
  { id: 'cerimonia', label: 'Local', icon: 'lucide:church' },
  { id: 'recepcao', label: 'Recepção', icon: 'lucide:cake-slice' },
  { id: 'calendario', label: 'Calendário', icon: 'lucide:calendar-heart' },
  { id: 'homem', label: 'Homem', icon: 'lucide:user' },
  { id: 'mulher', label: 'Mulher', icon: 'lucide:flower' },
  { id: 'teatro', label: 'Como nos conhecemos', icon: 'lucide:theater' },
  { id: 'namoro', label: 'Pedido de namoro', icon: 'lucide:message-square-heart' },
  { id: 'noivado', label: 'Pedido de noivado', icon: 'lucide:plane' },
];