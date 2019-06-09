import { ActiveUser } from "./ActiveUser.model";

export class Comment {
    CommentID: number;
    ClientID: number;
    ServiceID: number;
    Text: string;
    Deleted: boolean;
    Client: ActiveUser;
}