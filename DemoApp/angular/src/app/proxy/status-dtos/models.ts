import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateStatusDto {
  statusName: string;
}

export interface GetStatusListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface StatusDto extends EntityDto<string> {
  statusName?: string;
}

export interface UpdateStatusDto {
  statusName: string;
}
