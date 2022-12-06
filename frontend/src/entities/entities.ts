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



