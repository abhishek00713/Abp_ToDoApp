import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreatePriorityDto {
  priorityName: string;
}

export interface GetPriorityListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface PriorityDto extends EntityDto<string> {
  priorityName?: string;
}

export interface UpdatePriorityDto {
  priorityName: string;
}
