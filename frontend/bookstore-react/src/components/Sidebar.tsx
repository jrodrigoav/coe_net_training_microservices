import React from 'react';
import { NavLink } from 'react-router-dom';
import { routeItems } from '../routes/routeItems';


const Sidebar: React.FC = () => {
    return (
        <div className="d-flex flex-col flex-shrink-0 p-3 bg-light h-100 space-y-4">
            <div className="d-flex gap-2 align-items-center justify-content-md-center link-dark text-decoration-none fs-5">
                <span style={{ fontSize: '35px' }}> 📖 </span>
                <span style={{ fontSize: '20px'}}>Bookstore</span>
            </div>
            <hr />
            <ul className="nav nav-pills flex-column mb-auto">
                {routeItems.map((item) => (
                <li className="nav-item" key={item.path}>
                    <NavLink
                        to={item.path}
                        className={({ isActive }) =>
                        isActive ? 'active' : ''
                        }
                        style={{ fontSize: '18px', display: 'flex', alignItems: 'center', gap: '8px' }}
                        >
                        <span>{item.icon}</span>
                        <span>{item.title}</span>
                    </NavLink>
                </li>
            ))}
            </ul>
        </div>
    );
};

export default Sidebar;