import { Route, Routes } from 'react-router-dom';
import AdminLayout from '../components/AdminLayout';
import Resource from '../components/Resource';
import Client from '../components/Client';
import Inventory from '../components/Inventory';



const AdminLayoutRoutes = () => (
    <Routes>
        <Route path="/" element={<AdminLayout />}>
            <Route path="resources" element={<Resource />} />
            <Route path="clients" element={<Client />} />
            <Route path="inventory" element={<Inventory />} />
        </Route>
    </Routes>
);

export default AdminLayoutRoutes;
