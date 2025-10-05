import { Outlet } from 'react-router-dom';
import { useStateObject } from './utils/useStateObject';
import { useEffect, useState } from 'react';
import Header from './partials/Header';
import Footer from './partials/Footer';
import type { User } from './interfaces/BulletinBoard';

export default function App() {
  const [user, setUser] = useState<User | null>(null);
  const [state, setState] = useStateObject({
    categoryChoice: 'All',
    sortChoice: 'Newest first',
    searchTerm: '',
    showOnlyMyPosts: false
  });

  // Check login status on mount
  useEffect(() => {
    const checkLoginStatus = async () => {
      try {
        const response = await fetch('http://localhost:5002/api/login', {
          credentials: 'include'
        });
        if (response.ok) {
          const userData = await response.json();
          setUser(userData);
        } else {
          // 500 status with "No user is logged in" is expected when not logged in
          setUser(null);
        }
      } catch (error) {
        // Network error - user not logged in
        setUser(null);
      }
    };

    checkLoginStatus();
  }, []);

  return (
    <div className="d-flex flex-column min-vh-100">
      <Header user={user} setUser={setUser} />
      <main className="flex-grow-1" style={{ paddingTop: '80px' }}>
        <Outlet context={[state, setState, user, setUser]} />
      </main>
      <Footer />
    </div>
  );
}