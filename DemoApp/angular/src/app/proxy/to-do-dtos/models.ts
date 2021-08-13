import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { CategoryDto } from '../category-dtos/models';
import type { StatusDto } from '../status-dtos/models';
import type { PriorityDto } from '../priority-dtos/models';
import type { TaskDto } from '../task-dtos/models';

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
  category: CategoryDto;
  statusId?: string;
  status: StatusDto;
  priorityId?: string;
  priority: PriorityDto;
  taskId?: string;
  task: TaskDto;
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
