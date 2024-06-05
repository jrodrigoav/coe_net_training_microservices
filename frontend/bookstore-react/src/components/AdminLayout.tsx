import React from 'react';
import { Layout } from 'antd';
import Navbar from './Navbar';
import Sidebar from './Sidebar';
import { Outlet } from 'react-router-dom';

const { Header, Sider, Content } = Layout;

const AdminLayout: React.FC = () => {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider style={{ backgroundColor: '#e6f7ff' }}>
        <div className="logo" />
        <Sidebar />
      </Sider>
      <Layout className="site-layout" style={{ backgroundColor: '#e6f7ff' }}>
        <Header className="site-layout-background" style={{ padding: 0, backgroundColor: '#e6f7ff' }}>
          <Navbar />
        </Header>
        <Content style={{ margin: '0 16px', backgroundColor: '#e6f7ff' }}>
          <div className="site-layout-content">
            <Outlet />
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default AdminLayout;
