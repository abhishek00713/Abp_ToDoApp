import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateToDoDto {
  categoryId: string;
  statusId: string;
  priorityId: string;
  taskId: string;
  date: string;
  assignedBy: string;
  remarks?: string;
}

export interface GetAllToDoListDto extends PagedAndSortedResultRequestDto {
  filterCategory?: string;
  filterPriority?: string;
  filterStatus?: string;
  filterTask?: string;
}

export interface GetToDoListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface ToDoDto extends EntityDto<string> {
  categoryId?: string;
  category?: string;
  statusId?: string;
  status?: string;
  priorityId?: string;
  priority?: string;
  taskId?: string;
  task?: string;
  date?: string;
  assignedBy?: string;
  remarks?: string;
}

export interface UpdateToDoDto {
  categoryId: string;
  statusId: string;
  priorityId: string;
  taskId: string;
  date: string;
  assignedBy: string;
  remarks?: string;
}
