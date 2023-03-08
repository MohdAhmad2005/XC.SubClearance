import { SubmissionStatus } from "src/app/enum/submission/submission-status";

export interface SubmissionAtGlance {
    status: SubmissionStatus;
    count: number;
}
