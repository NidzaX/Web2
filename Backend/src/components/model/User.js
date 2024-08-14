class User {
    constructor(id, firstName, lastName, email, username, password, address, birthday, userType, file, verified) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.username = username;
        this.password = password;
        this.address = address;
        this.birthday = birthday;
        this.userType = userType;
        this.file = file;
        this.verified = verified;
    }

    static create({ firstName, lastName, email, username, password, address, birthday, userType, file }) {
        return new User(
            crypto.randomUUID(), // Simulating Guid.NewGuid() in C#
            firstName,
            lastName,
            email,
            username,
            password,
            address,
            birthday,
            userType,
            file,
            false // Assuming new users are not verified by default
        );
    }
}

export default User;
