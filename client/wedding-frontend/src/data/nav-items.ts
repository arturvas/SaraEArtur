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
];