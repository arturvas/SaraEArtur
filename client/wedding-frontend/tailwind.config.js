/** @type {import('tailwindcss').Config} */
export default {
  content: ['./src/**/*.{astro,html,js,jsx,ts,tsx}'],
  theme: {
    extend: {
      colors: {
        serenity: {
          DEFAULT: '#89ABE3',
          light: '#e5efff',
          dark: '#1f3a5f',
        },
      },
    },
  },
  plugins: [],
};
