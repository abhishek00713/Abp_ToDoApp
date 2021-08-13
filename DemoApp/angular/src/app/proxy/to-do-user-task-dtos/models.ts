import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateToDoUserTaskDto {
  toDoId: string;
  statusId: string;
}

export interface GetToDoUserTaskListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface ToDoUserTaskDto extends EntityDto<string> {
  toDoId?: string;
  statusId?: string;
}

export interface UpdateToDoUserTaskDto {
  toDoId: string;
  statusId: string;
}
