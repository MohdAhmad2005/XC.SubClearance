export class SubmissionExtraction {
  id: string;
  submissionId: string;
  submissionFormData: SubmissionFormData;
}

export class SubmissionFormData {
  submissionData: SubmissionData[]
}

export class SubmissionData {
  fields: string
  groupName: string
  suggestions: Suggestions
  finalEntry: string
  confidance: string
}

export class Suggestions {
  id: string
  suggestionOptions: SuggestionOption[]
}

export class SuggestionOption {
  id: string
  name: string
  finalEntry: string
  confidance: string
}
