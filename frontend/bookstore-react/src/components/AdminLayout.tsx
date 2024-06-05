import Navbar from './Navbar';
import Sidebar from './Sidebar';
import { Outlet } from 'react-router-dom';

const AdminLayout = () => {
  return (
    <div className="main-wrapper">
      <Sidebar />
      <div>
        <Navbar />
        <div className="router-content">
          <Outlet />
        </div>
      </div>
    </div>
  );
};

export default AdminLayout;
