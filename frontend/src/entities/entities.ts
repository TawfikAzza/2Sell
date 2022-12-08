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
  name: string;
  ticked: boolean;
  categories: Category[];
}

export interface categoryDTO{
  ids: number[]
}

export interface priceDTO{
  min: number,
  max: number;
}

export interface catPriceDTO{
  ids: number[],
  min: number,
  max: number
}

export interface searchDTO{
  args: string
}

export interface filterSearchDTO{
  operationType: number,
  dto: categoryDTO | priceDTO | catPriceDTO | searchDTO
}

