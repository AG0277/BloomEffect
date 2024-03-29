.data   
    myFloat1 REAL4 3.00
    thresholdValue dd 191
    myInt dd 4 
    two dd 2
    radiusLenght dd 0
.code

MyProc1 proc

    xor rax, rax           
    mov rbx, r8 
    mov r10d, [myInt]
    imul rbx, r10
         ; passed in rcx originalPixels,
         ; passed in rdx  bloomPixels,   
         ; passed in r8  width,  
         ; passed in r9  height 
         ; r10 bytesperpixel 

    outer_loop:
        mov R11, 0          ; Clear r11 to store resultIndex
         
        inner_loop:
            movzx r12, byte ptr [rcx  + r11]           
            movzx r13, byte ptr [rcx  + r11 + 1]       
            movzx r14, byte ptr [rcx  + r11 + 2]       
            xor rax, rax  
            add rax, r12
            add rax, r13
            add rax, r14

            vpxor xmm0,xmm0, xmm0
            vpxor xmm1,xmm1, xmm1
            cvtsi2ss xmm1, rax
            movss xmm0, dword ptr [myFloat1]
            divps xmm1, xmm0
            CVTPS2DQ xmm1,xmm1

            movss xmm2, dword ptr [thresholdValue]
            comiss xmm1, xmm2
            ja greater_than_191
            mov byte ptr [rdx + r11] , 0
            mov byte ptr [rdx + r11 + 1] , 0
            mov byte ptr [rdx + r11 + 2] , 0
            mov byte ptr [rdx + r11 + 3] , 255
            jmp skip_191


            greater_than_191:
            mov  [rdx + r11] , r12
            mov  [rdx + r11 + 1] , r13
            mov  [rdx + r11 + 2] , r14
            mov byte ptr byte ptr [rdx + r11 + 3] , 255

            skip_191:
            ; Update resultIndex
            add R11, R10

            ; Continue inner loop
            cmp R11, rbx
            jl inner_loop

        ; Update pointers for the next row
        add rcx, rbx
        add rdx, rbx

        ; Continue outer loop
        dec r9
        jnz outer_loop
         xor rax, rax  
ret
MyProc1 endp








.code
MyProc2 PROC
    ; Arguments:
    ; rcx = sourcePixels 
    ; rdx = destPixels 
    ; r8  = x 
    ; r9  = y 
    ; r10 = kernel 
    ; r11d = radius 
    ; r15d = stride 
    ; r13d = height 


    mov eax, r11d
    shl eax, 1
    mov [radiusLenght], eax
    pxor xmm2, xmm2  ; red

    mov rax, [rsp + 40] 
    mov r10, rax 

    mov rax, [rsp + 48] 
    mov r11, rax 

    mov rax, [rsp + 56] 
    mov r15, rax 

    mov rax, [rsp + 64] 
    mov r13, rax 
   

    ; Loop over the kernel
    xor esi, esi  
    loop_kernel:
        ;  yOffset = y + i - radius
        mov eax, r9d  ; Copy y to eax
        add eax, esi  ; Add i
        sub eax, r11d  ; Subtract radius
        ; 0 <= yOffset < height
        cmp eax, 0
        jl skip_pixel
        cmp eax, r13d
        jge skip_pixel

        ; index = yOffset * stride + x * bytesPerPixel
        mov ebx, eax
        imul ebx, r15d
        lea ebx, [rbx + r8 * 4] 

        ; Load the weight from the kernel (double-precision)
        movsd xmm3, qword ptr [r10 + rsi * 8] 
        CVTPD2PS xmm3, xmm3 
        shufps xmm3,xmm3, 00h
        
        movzx r14, byte ptr[rcx+rbx+2]
        PINSRQ xmm5, r14,1
        movzx r14d, byte ptr[rcx+rbx+1]
        PINSRD xmm5, r14d,1
        movzx r14d,byte ptr[rcx+rbx]
        PINSRD xmm5, r14d,0
        cvtdq2ps xmm5, xmm5 ; convert to float

        mulps xmm5, xmm3
        addps xmm1, xmm5


        skip_pixel:
        xor eax,eax
        inc esi
        cmp esi, [radiusLenght] 
        jl loop_kernel



CVTPS2DQ xmm1,xmm1 
mov eax, r8d          
imul eax, 4           
imul r9d, r15d         
add eax, r9d


movd ebx,xmm1
mov [rdx+rax], ebx
shufps xmm1, xmm1, 39h

movd ebx,xmm1
mov [rdx+rax+1], ebx
shufps xmm1, xmm1, 39h

movd ebx,xmm1
mov [rdx+rax+2], ebx

mov byte ptr[rdx+rax+3], 255

    ret
MyProc2 ENDP


.code
MyProc3 PROC
    ; Arguments:
    ; rcx = sourcePixels (pointer to source pixel data)
    ; rdx = destPixels (pointer to destination pixel data)
    ; r8  = x (current x-coordinate)
    ; r9  = y (current y-coordinate)
    ; r10 = kernel (pointer to kernel array of doubles)
    ; r11d = radius (use a 32-bit register for radius)
    ; r15d = stride (stride of the image)
    ; r13d = width (height of the image)
    ; xmm3 kernel
    ; xmm2 getPixel
    ; xmm1 overall pixel

    mov eax, r11d
    shl eax, 1
    mov [radiusLenght], eax
    pxor xmm2, xmm2  ; red

    mov rax, [rsp + 40] 
    mov r10, rax 

    mov rax, [rsp + 48] 
    mov r11, rax 

    mov rax, [rsp + 56] 
    mov r15, rax 

    mov rax, [rsp + 64] 
    mov r13, rax 
   

    ; Loop over the kernel
    xor esi, esi  ; esi = i (loop counter)
    loop_kernel:
        ; xOffset = x + i - radius
        mov eax, r8d  ; Copy x to eax
        add eax, esi  ; Add i
        sub eax, r11d  ; Subtract radius
        ; 0 <= xOffset < width
        cmp eax, 0
        jl skip_pixel
        cmp eax, r13d
        jge skip_pixel

        ;  index =y * stride + xOffSet * bytesPerPixel
        mov ebx, r9d
        imul ebx, r15d
        imul eax, [myInt]
        add ebx, eax

        ; Load the weight from the kernel (double-precision)
        movsd xmm3, qword ptr [r10 + rsi * 8] ; Load kernel weight
        CVTPD2PS xmm3, xmm3 ; convert double to float
        shufps xmm3,xmm3, 00h
        
        movzx r14, byte ptr[rcx+rbx+2]
        PINSRQ xmm5, r14,1
        movzx r14d, byte ptr[rcx+rbx+1]
        PINSRD xmm5, r14d,1
        movzx r14d,byte ptr[rcx+rbx]
        PINSRD xmm5, r14d,0
        cvtdq2ps xmm5, xmm5 ; convert to float

        mulps xmm5, xmm3
        addps xmm1, xmm5


        skip_pixel:
        xor eax,eax
        inc esi
        cmp esi, [radiusLenght] 
        jl loop_kernel



CVTPS2DQ xmm1,xmm1 ; convert from float
mov eax, r8d           ; Copy x-coordinate to eax
imul eax, 4            ; Multiply by 4 (assuming 4 bytes per pixel)
imul r9d, r15d         ; Multiply by the stride
add eax, r9d


movd ebx,xmm1
mov [rdx+rax], ebx
shufps xmm1, xmm1, 39h

movd ebx,xmm1
mov [rdx+rax+1], ebx
shufps xmm1, xmm1, 39h

movd ebx,xmm1
mov [rdx+rax+2], ebx

mov byte ptr[rdx+rax+3], 255

    ret
MyProc3 ENDP
end

