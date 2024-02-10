class User {
    id:number;
    login:string;
    role:UserRole;
    characters:Character[];

    constructor(id:number, login:string, role:UserRole, characters:Character[]) {
        this.id = id;
        this.login = login;
        this.role = role;
        this.characters = characters;
    }
}