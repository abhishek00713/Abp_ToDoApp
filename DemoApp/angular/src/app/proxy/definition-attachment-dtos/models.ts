import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateDefinitionAttachmentDto {
  toDoId: string;
  caption: string;
  fileName?: string;
  binaryFile: number[];
}

export interface DefinitionAttachmentDto extends EntityDto<string> {
  todoId?: string;
  caption?: string;
  fileName?: string;
}

export interface GetDefinitionAttachmentListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateDefinitionAttachmentDto {
  caption: string;
  fileName?: string;
  binaryFile: number[];
}
