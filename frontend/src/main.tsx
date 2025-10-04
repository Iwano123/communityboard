import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import routes from './routes';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import App from './App';
import '../sass/index.scss';

// Register service worker for PWA (temporarily disabled to avoid fetch errors)
/*
if ('serviceWorker' in navigator) {
  window.addEventListener('load', () => {
    navigator.serviceWorker.register('/sw.js')
      .then((registration) => {
        console.log('SW registered: ', registration);
      })
      .catch((registrationError) => {
        console.log('SW registration failed: ', registrationError);
      });
  });
}
*/

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: routes.map(route => {
      if (route.index) {
        return {
          index: true,
          element: route.element,
          loader: route.loader
        };
      } else {
        return {
          path: route.path,
          element: route.element,
          loader: route.loader
        };
      }
    }),
    errorElement: <div>Something went wrong!</div>,
    hydrateFallbackElement: <div>Loading...</div>
  }
], {
  future: {
    v7_fetcherPersist: true,
    v7_normalizeFormMethod: true,
    v7_partialHydration: true,
    v7_relativeSplatPath: true,
    v7_skipActionErrorRevalidation: true,
  }
});

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);