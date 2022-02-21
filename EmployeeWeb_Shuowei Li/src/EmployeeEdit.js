import React, { Component } from 'react';
import { Link, withRouter } from 'react-router-dom';
import { Button, Container, Form, FormGroup, Input, Label } from 'reactstrap';
import AppNavbar from './AppNavbar';

class EmployeeEdit extends Component {

    emptyItem = {
        name: '',
        email: ''
    };

    constructor(props) {
        super(props);
        this.state = {
            item: this.emptyItem
        };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    async componentDidMount() {
        if (this.props.match.params.id !== 'new') {
            const employee = await (await fetch(`http://localhost:8080/employees/${this.props.match.params.id}`)).json();
            this.setState({ item: employee });
        }
    }

    handleChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        let item = { ...this.state.item };
        item[name] = value;
        this.setState({ item });
    }

    async handleSubmit(event) {
        event.preventDefault();
        const { item } = this.state;

        await fetch('http://localhost:8080/employees' + (item.id ? '/' + item.id : ''), {
            method: (item.id) ? 'PUT' : 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item),
        });
        alert("Success!");
        this.props.history.push('/employees');
    }

    render() {
        const { item } = this.state;
        const title = <h2>{item.id ? 'Edit Employee' : 'Add Employee'}</h2>;

        return <div>
            <AppNavbar />
            <Container>
                {title}
                <Form onSubmit={this.handleSubmit}>
                    <FormGroup>
                        <Label for="name">Name</Label>
                        <Input type="text" name="name" id="name" value={item.name || ''}
                            onChange={this.handleChange} autoComplete="name" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="phone">Phone</Label>
                        <Input type="text" name="phone" id="phone" value={item.phone || ''}
                            onChange={this.handleChange} autoComplete="phone" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="department">Department</Label>
                        <Input type="text" name="department" id="department" value={item.department || ''}
                            onChange={this.handleChange} autoComplete="department" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="street">Address: Street</Label>
                        <Input type="text" name="street" id="street" value={item.street || ''}
                            onChange={this.handleChange} autoComplete="street" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="city">Address: City</Label>
                        <Input type="text" name="city" id="city" value={item.city || ''}
                            onChange={this.handleChange} autoComplete="city" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="state">Address: State</Label>
                        <Input type="text" name="state" id="state" value={item.state || ''}
                            onChange={this.handleChange} autoComplete="state" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="zip">Address: Zip</Label>
                        <Input type="text" name="zip" id="zip" value={item.zip || ''}
                            onChange={this.handleChange} autoComplete="zip" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="country">Address: Country</Label>
                        <Input type="text" name="country" id="country" value={item.country || ''}
                            onChange={this.handleChange} autoComplete="country" />
                    </FormGroup>
                    <FormGroup>
                        <Button color="primary" type="submit">Save</Button>{' '}
                        <Button color="secondary" tag={Link} to="/employees">Cancel</Button>
                    </FormGroup>
                </Form>
            </Container>
        </div>
    }
}

export default withRouter(EmployeeEdit);