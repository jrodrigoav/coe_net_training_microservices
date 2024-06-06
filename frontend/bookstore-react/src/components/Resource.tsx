import React, { useEffect, useState } from 'react';
import { Button, Table, Modal, Form, Input } from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { useInventoryContext } from '../contexts/InventoryContext';
import { useResourceContext } from '../contexts/ResourceContext';
import { Resource } from '../interfaces/Resource';

const ResourceComponent: React.FC = () => {
  const { resources, getResourceList, createResource, updateResource, deleteResource } = useResourceContext();
  const { registerInventoryResource } = useInventoryContext();

  const [searchString, setSearchString] = useState<string>('');
  const [filteredResources, setFilteredResources] = useState<Resource[]>([]);
  const [modalVisible, setModalVisible] = useState<boolean>(false);
  const [modalTitle, setModalTitle] = useState<string>('');
  const [resourceForm, setResourceForm] = useState<Resource>({
    id: '',
    name: '',
    description: '',
    dateOfPublication: new Date(),
    author: '',
    tags: [],
    type: ''
  });

  // Fetch data on mount
  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    setFilteredResources(resources.filter(resource => resource.name.toLowerCase().includes(searchString.toLowerCase())));
  }, [resources, searchString]);

  const fetchData = async () => {
    try {
      await getResourceList();
    } catch (error) {
      console.error('Error fetching resources:', error);
    }
  };

  const handleDeleteResource = async (id: string) => {
    try {
      if (id) {
        await deleteResource(id);
        fetchData();
      }
    } catch (error) {
      console.error('Error deleting resource:', error);
    }
  };

  const handleRegister = async (id: string) => {
    try {
      await registerInventoryResource(id);
    } catch (error) {
      console.error('Error registering resource:', error);
    }
  };

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchString(e.target.value);
  };

  const handleResourceFormChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    if (name === 'dateOfPublication') {
      setResourceForm({ ...resourceForm, dateOfPublication: new Date(value) });
    } else if (name === 'tags') {
      setResourceForm({ ...resourceForm, tags: value.split(',').map(tag => tag.trim()) });
    } else {
      setResourceForm({ ...resourceForm, [name]: value });
    }
  };

  const submitResourceModal = async () => {
    if (resourceForm.id) {
      await updateResource(resourceForm.id, resourceForm);
    } else {
      await createResource(resourceForm);
    }
    fetchData();
    setModalVisible(false);
  };

  const onNew = () => {
    setModalTitle("New Resource");
    setResourceForm({
      id: '',
      name: '',
      description: '',
      dateOfPublication: new Date(),
      author: '',
      tags: [],
      type: ''
    });
    setModalVisible(true);
  };

  const onUpdate = (resource: Resource) => {
    setModalTitle("Update Resource");
    setResourceForm(resource);
    setModalVisible(true);
  };

  return (
    <div className="main-content">
      <Button type="primary" icon={<PlusOutlined />} onClick={onNew} style={{ marginLeft: '10px' }}>
        New
      </Button>
      <div className="container-fluid">
        <div className="flex flex-col gap-4 p-4">
          <Input
            placeholder="Type to search..."
            value={searchString}
            onChange={handleSearch}
            className="w-full"
          />
          <Table dataSource={filteredResources} rowKey="id" className="mt-4">
            <Table.Column title="ID" dataIndex="id" key="id" />
            <Table.Column title="Name" dataIndex="name" key="name" />
            <Table.Column title="Description" dataIndex="description" key="description" />
            <Table.Column
              title="Actions"
              key="actions"
              render={(record: Resource) => (
                <div className="flex gap-2">
                  <Button
                    icon={<EditOutlined />}
                    onClick={() => onUpdate(record)}
                  >
                    Edit
                  </Button>
                  <Button
                    icon={<DeleteOutlined />}
                    onClick={() => handleDeleteResource(record.id)}
                  >
                    Delete
                  </Button>
                  <Button onClick={() => handleRegister(record.id)}>
                    Register
                  </Button>
                </div>
              )}
            />
          </Table>
        </div>
      </div>

      <Modal
        title={modalTitle}
        open={modalVisible}
        onCancel={() => setModalVisible(false)}
        footer={[
          <Button key="cancel" onClick={() => setModalVisible(false)}>Cancel</Button>,
          <Button key="submit" type="primary" onClick={submitResourceModal}>Ok</Button>,
        ]}
      >
        <Form layout="vertical">
          <Form.Item label="Name">
            <Input
              name="name"
              value={resourceForm.name}
              onChange={handleResourceFormChange}
            />
          </Form.Item>
          <Form.Item label="Description">
            <Input
              name="description"
              value={resourceForm.description}
              onChange={handleResourceFormChange}
            />
          </Form.Item>
          <Form.Item label="Date of Publication">
            <Input
              type="date"
              name="dateOfPublication"
              value={resourceForm.dateOfPublication.toISOString().substring(0, 10)}
              onChange={handleResourceFormChange}
            />
          </Form.Item>
          <Form.Item label="Author">
            <Input
              name="author"
              value={resourceForm.author}
              onChange={handleResourceFormChange}
            />
          </Form.Item>
          <Form.Item label="Tags">
            <Input
              name="tags"
              value={resourceForm.tags.join(', ')}
              onChange={(e) => setResourceForm({ ...resourceForm, tags: e.target.value.split(',').map(tag => tag.trim()) })}
            />
          </Form.Item>
          <Form.Item label="Type">
            <Input
              name="type"
              value={resourceForm.type}
              onChange={handleResourceFormChange}
            />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default ResourceComponent;