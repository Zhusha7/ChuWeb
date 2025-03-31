/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'accent-pink': '#ff71ce',
        'accent-blue': '#01cdfe',
        'accent-yellow': '#ffda00',
      },
      borderRadius: {
        'bevel': '12px',
      },
      fontFamily: {
        'sans': ['Inter', 'system-ui', 'sans-serif'],
      },
    },
  },
  plugins: [],
} 