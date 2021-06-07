import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface AssignedToUserDto extends EntityDto<string> {
  userId: string;
  toDoId: string;
  isActive: boolean;
}

export interface CreateAssignedToUserDto {
  userId: string;
  toDoId: string;
  isActive: boolean;
}

export interface GetAssignedToUserListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateAssignedToUserDto {
  isActive: boolean;
}
