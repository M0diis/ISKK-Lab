/**
 * Post entity for lists.
 */
interface PostForList
{
    id: number;
    title: string;
    content: string;
    createdTimestamp: string;
    fK_UserID: number;
    userName: string;
}

export type {
    PostForList
}

/**
 * Review entity for lists.
 */
interface ReviewForList
{
    id: number;
    data: string;
    createdTimestamp: string;
    fK_UserID: number;
    userName: string;
}

export type {
    ReviewForList
}

/**
 * Response to valid login request.
 */
interface LogInResponse
{
    userId: number;
    userTitle: string;
    jwt: string;
    isAdmin: boolean;
};

export type {
    LogInResponse
}

/**
 * Ticket entity for lists.
 */
interface TicketForList
{
    id: number;
    title: string;
    description: string;
    createdTimestamp: string;
    closed: boolean;
    fK_UserID: number;
    userName: string;
}

export type {
    TicketForList
}