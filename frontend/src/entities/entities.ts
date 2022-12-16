export interface registerDTO{
  email:string,
  password:string,
  firstName:string,
  lastName:string,
  userName:string,
  address:string,
  postalCode:string,
  phoneNumber:string,
  img?:string,
  roleId:number
}
export interface CommentDTO {
  postId:number,
  content:string,
  author:string,
  date:string,
  avatar:string
}
export interface loginDTO{
  Email: string,
  Password: string
}
export interface postDTO{
  id:number,
  email:string,
  userName:string,
  price:number,
  title:string,
  description:string,
  authority:number,
  address:string,
  category:number
  img:string
}
export interface MailDTO {
  receiver:string,
  sender:string,
  subject:string,
  mail_content:string,
  receiverName:string,
  senderName:string
}
export interface createPostDTO {
  Email:string,
  Title: string,
  Description: string,
  Price: number,
  Category: number,
  Img:string
}
export interface sessionToken{
  exp?: number;
  role?: number;
  userName?:string;
  email?:string;
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
export interface UserProperties{
  userName:string,
  email:string,
  roleId:number
}

