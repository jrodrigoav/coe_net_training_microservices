import { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { routeItems } from '../routes/routeItems';

const Navbar = () => {
  const [isExpanded, setIsExpanded] = useState(false);
  const [title, setTitle] = useState('Dashboard');
  const location = useLocation();

  useEffect(() => {
    const activeRoute = routeItems.find(item => location.pathname === item.path);
    setTitle(activeRoute ? activeRoute.title : 'Dashboard');
  }, [location]);

  const toggleNavbar = () => {
    setIsExpanded(!isExpanded);
  };

  return (
    <nav className="navbar navbar-light bg-light">
      <div className="container-fluid">
        <button type="button" className="navbar-toggler d-md-none" onClick={toggleNavbar}>
          <i className="bi bi-list"></i>
        </button>
        <span className="navbar-brand" style={{ fontSize: '24px', marginLeft: '20px' }}>{title}</span>
      </div>
    </nav>
  );
};

export default Navbar;