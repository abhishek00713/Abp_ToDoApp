import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateToDo_Assigned {
  toDoId: string;
  assignedTo: string;
}

export interface GetToDo_Assigned extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface ToDoAssignedDto extends EntityDto<string> {
  toDoId?: string;
  assignedTo?: string;
}

export interface UpdateToDo_Assigned {
  toDoId: string;
  assignedTo: string;
}
