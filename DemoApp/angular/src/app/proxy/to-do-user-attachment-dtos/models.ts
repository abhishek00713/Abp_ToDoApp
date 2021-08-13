import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateToDoUserAttachmentDto {
  toDOUserTaskId: string;
  attachmentCaption: string;
  attachmentFile: string;
  binaryFile: number[];
}

export interface GetToDoUserAttachmentListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface ToDoUserAttachmentDto extends EntityDto<string> {
  toDOUserTaskId?: string;
  attachmentCaption?: string;
  attachmentFile?: string;
}

export interface UpdateToDoUserAtatchmentDto {
  toDOUserTaskId: string;
  attachmentCaption: string;
  attachmentFile: string;
  binaryFile: number[];
}
