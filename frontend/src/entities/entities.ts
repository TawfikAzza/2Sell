export interface registerDTO{
  email:string,
  password:string,
  firstName:string,
  lastName:string,
  userName:string,
  address:string,
  postalCode:string,
  phoneNumber:string,
  roleID:number
}

export interface loginDTO{
  email: string,
  password: string
}

export interface sessionToken{
  expDate?: number;
  role?: number;
}

export interface Category{
  id:number;
  name:string;
  ticked: boolean;
}

export interface NavBarSearch {
  operationType:number;
  name: string;
  ticked: boolean;
  categories: Category[];
}

