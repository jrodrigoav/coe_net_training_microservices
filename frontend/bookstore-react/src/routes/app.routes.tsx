import { Route, Routes, Navigate } from 'react-router-dom';
import AdminLayoutRoutes from './admin-layout.routes';

const AppRouter = () => (
    <Routes>
        <Route path="/" element={<Navigate to="clients" />} />
        <Route path="/*" element={<AdminLayoutRoutes />} />
    </Routes>
);

export default AppRouter;