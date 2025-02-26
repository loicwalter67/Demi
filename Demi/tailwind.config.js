/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './**/*.{razor,html}',
    ],
  theme: {
      extend: {
          colors: {
              primary: 'var(--color-primary)',
              secondary: 'var(--color-secondary)',
          }
      },
  },
  plugins: [],
}

