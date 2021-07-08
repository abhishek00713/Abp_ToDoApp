import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateTaskDto {
  taskName: string;
}

export interface GetTaskListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface TaskDto extends EntityDto<string> {
  taskName?: string;
}

export interface UpdateTaskDto {
  taskName: string;
}
